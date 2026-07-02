using System;
using System.Collections;
using Combat.CombatAttacks;
using GameFlow.EventChannels.SpecificEC;
using Pathfinding;
using Unity.Mathematics;
using UnityEngine;
using UOP1.StateMachine.ScriptableObjects;
using Random = UnityEngine.Random;

namespace Entities.Enemies.Pathfinding {
    [RequireComponent(typeof(Seeker))]
    public abstract class EnemyAI : MonoBehaviour {
        [Header("Enemy Movesets")] 
        [SerializeField] private EnemyMoveset[] enemyMovesets;
        
        [Header("General Variables")]
        public float nextWaypointDistance = 1;
        public float visionArea = 10;
        public float actionArea = 2;
        public float cooldown = 3;

        protected Transform targetPosition;
        
        [NonSerialized] public Seeker seeker;
        [NonSerialized] public Path path;
        [NonSerialized] public bool reachedEndOfPath;
        [NonSerialized] public int currentWaypoint;
        
        [NonSerialized] public float timeOfLastMove;
        
        public delegate Vector2 PathfindingPositionCallback();
        public event PathfindingPositionCallback OnPathfindingPositionRequested;

        private Vector2 RequestPathfindingPosition() {
            return OnPathfindingPositionRequested != null ? OnPathfindingPositionRequested.Invoke() : Vector2.zero;
        }
        

        private void Start() {
            seeker = GetComponent<Seeker>();
            timeOfLastMove = Single.NegativeInfinity;

            targetPosition = GameObject.FindWithTag("Player").transform;
            // InvokeRepeating(nameof(UpdatePath), 0, 0.3f);
        }

        public void StartPathfinding() {            
            path = null;
            currentWaypoint = 0;
            reachedEndOfPath = false;
            StartCoroutine(UpdatePath(0.3f));
        }

        public void StopPathfinding() {
            StopCoroutine(UpdatePath(0.3f));
            reachedEndOfPath = false;
            currentWaypoint = 0;
            path = null;
        }

        private IEnumerator UpdatePath(float waitForSeconds) {
            while (true) {
                reachedEndOfPath = false;
                seeker.CancelCurrentPathRequest();
                
                Vector2 pathfindingPosition = RequestPathfindingPosition();
                if (pathfindingPosition == Vector2.zero)
                    reachedEndOfPath = true;
                else
                    seeker.StartPath(transform.position, pathfindingPosition, OnPathComplete);
                
                yield return new WaitForSeconds(waitForSeconds);
            }
        }

        protected abstract Vector2 GetDestinationPosition();

        private void OnPathComplete (Path p) {
            if (p.error) return;
            path = p;
            currentWaypoint = 0;
        }

        public StateSO SelectAction(EnemyMovesetType movesetType) {
            foreach (var moveset in enemyMovesets) {
                if (moveset.movesetType == movesetType) {
                    CombatMovesetSO enemyMoveset = moveset.enemyMoveset;
                    
                    float moveValue = Random.Range(0f, 1f);
                    float moveChance = 0;
            
                    foreach (CombatMove combatMove in enemyMoveset.combatMoves) {
                        moveChance += combatMove.attackChance;
                        if (moveValue < moveChance)
                            return combatMove.attackState;
                    }
                }
            }

            return null;
        }

        public bool PlayerInVision() {
            return Vector2.Distance(transform.position, targetPosition.position) <= visionArea;
        }
    }

    public enum EnemyMovesetType {
        NormalAttack, DefensiveAttack, PanicAttack,
        FollowMove, BackUpMove, CircleMove, Transition
    }
    
    [Serializable]
    public class EnemyMoveset {
        public EnemyMovesetType movesetType;
        public CombatMovesetSO enemyMoveset;
    }
}