using System;
using Combat.CombatAttacks;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;

namespace Entities {
    public abstract class Controller : MonoBehaviour {
        [Header("Controller Data")] 
        public ControllerDataSO data;
        
        [Header("Animators")] 
        public Animator spriteAnimator;
        [NonSerialized] public bool finishAnimation;
        
        [Header("Rendered Object Transforms")]
        public Transform spriteTransform;
        [NonSerialized] public Transform characterTransform;

        [Header("Combat Channel")] 
        [SerializeField] protected CombatChannel combatChannel;
        
        // Moving Variables
        [NonSerialized] public Vector2 adjustedGroundMovement;
        [NonSerialized] public Vector2 movementVector;
        
        // Jump Variables
        [NonSerialized] public float aerialMovement;
        [NonSerialized] public float characterWeight;
        
        // Combat Variables
        [NonSerialized] public Vector2 attackAlignmentSize;
        [NonSerialized] public Vector2 attackColliderSize;
        
        // Scaling Variables
        [NonSerialized] public float scaleDirection = 1;
        [NonSerialized] public float scaleInclineBound;
        
        // Slope Variables
        [NonSerialized] public float groundSlope;
        [NonSerialized] public float movementSpeed;

        protected virtual void Awake() {
            movementSpeed = data.walkSpeed;
            characterWeight = data.characterWeight;
            characterTransform = transform;
            attackAlignmentSize = new Vector2(5.6f, 0.4f);
            attackColliderSize = new Vector2(3f, 3f);
        }
        
        public void StatusMove(CombatStatusMoveSO combatStatusMove) {
            combatStatusMove.PerformMove(combatChannel);
        }

        public void ModifyAttackValue(float value) {
            // NOTHING BURGER
        }
    }
}