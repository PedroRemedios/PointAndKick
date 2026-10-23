using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Combat.CombatAttacks;
using Entities.HealthStatus.Damageables;
using UnityEngine;
using UOP1.StateMachine.ScriptableObjects;

namespace Entities.Enemies {
    public class EnemyController : Controller {
        [Header("Player Transform")]
        [NonSerialized] public Transform playerTransform;
    
        // Movement Variables
        [NonSerialized] public Vector2 rawGroundMovement;

        // Combat Variables
        [NonSerialized] public StateSO moveSelected;
        
        // Control Variables
        [NonSerialized] public bool isFacingLeft = true;
        [NonSerialized] public bool startedCombat = false;
        [NonSerialized] public float transitionTime;

        protected override void Awake() {
            base.Awake();
            playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void Update() {
            AdjustGroundMovement();
            // TODO: SCALING
        }
    
        private void AdjustGroundMovement() {
            adjustedGroundMovement = new Vector2(rawGroundMovement.x, rawGroundMovement.y + rawGroundMovement.x * groundSlope).normalized;
        }
        
        /*
         * DAMAGE DEALER 
         */

        public void SelectAction(StateSO moveState) {
            moveSelected = moveState;
        }
    
        public void DealDamage(CombatAttackSO combatAttack) {
            bool hitSomething = false;

            GameObject[] alignedPlayers = GetAlignedPlayers();
            GameObject[] hitPlayers = GetHitPlayers(combatAttack);
        
            var playersToAttack = alignedPlayers.Intersect(hitPlayers).ToList();
            foreach (GameObject playerGameObject in playersToAttack) {
                PlayerDamageable playerDamageable = playerGameObject.transform.GetComponent<PlayerDamageable>();
                playerDamageable.TakeDamage(combatAttack, spriteTransform);
                Instantiate(combatAttack.hitParticle,
                    (Vector2) spriteTransform.position + combatAttack.hitParticleOffset * spriteTransform.lossyScale,
                    spriteTransform.rotation);
                hitSomething = true;
            }

            if (!hitSomething) return;
            spriteAnimator.enabled = false;
            StartCoroutine(ResumeAnimation(playersToAttack));
        }

        public bool IsAbleToHit() {
            GameObject[] alignedPlayers = GetAlignedPlayers();
            GameObject[] hitPlayers = GetHitPlayers();
        
            var playersToAttack = alignedPlayers.Intersect(hitPlayers).ToList();

            return playersToAttack.Count > 0;
        }
    
        private GameObject[] GetAlignedPlayers() {
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(transform.position, attackAlignmentSize, 0f, Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Player"));
            return raycastHits.Select(raycastHit => raycastHit.transform.gameObject).ToArray();
        }
        
        private GameObject[] GetHitPlayers() {
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(transform.position, attackColliderSize, 0f, Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Character"));
            return raycastHits.Select(raycastHit => raycastHit.transform.gameObject).ToArray();
        }

        private GameObject[] GetHitPlayers(CombatAttackSO combatAttack) {
            Vector2 attackPosition = (Vector2) transform.position + combatAttack.attackPositionOffset * transform.lossyScale;
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(attackPosition, combatAttack.attackColliderArea, 0f, Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Character"));
            return raycastHits.Select(raycastHit => raycastHit.transform.gameObject).ToArray();
        }
        
    
        private IEnumerator ResumeAnimation(List<GameObject> attackedPlayers) {
            yield return new WaitForSeconds(2/24f);
            spriteAnimator.enabled = true;
            
            foreach (GameObject playerGameObject in attackedPlayers) {
                PlayerDamageable playerDamageable = playerGameObject.transform.GetComponent<PlayerDamageable>();
                playerDamageable.resumingKnocked = true;
            }
        }
    }
}
