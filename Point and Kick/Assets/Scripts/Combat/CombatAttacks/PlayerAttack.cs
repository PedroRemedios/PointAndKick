using System.Collections;
using System.Linq;
using Entities.HealthStatus.Damageables;
using UnityEngine;

namespace Combat.CombatAttacks {
    public class PlayerAttack : Attack {
        private float previousMovementSpeed;
        private float previousWeight;
        private float previousAerial;
        
        private void OnTriggerEnter2D(Collider2D col) {
            if (col.CompareTag("Enemy GFX")) {
                bool hitSomething = false;
                Damageable _damageable = col.GetComponentInParent<Damageable>();
                Damageable[] _alignedDamageables = GetAlignedEnemies();

                if (_alignedDamageables.Contains(_damageable) && !hitDamageables.Contains(_damageable)) {
                    hitDamageables.Add(_damageable);
                    _damageable.TakeDamage(combatAttack, controller.spriteTransform);
                    
                    _damageable.resumingKnocked = true;
                    //Instantiate(combatAttack.hitParticle,
                    //    (Vector2) controller.spriteTransform.position + combatAttack.hitParticleOffset * controller.spriteTransform.lossyScale,
                    //    controller.spriteTransform.rotation);
                    hitSomething = true;
                }

                if (!hitSomething) return;
                controller.spriteAnimator.enabled = false;

                previousMovementSpeed = controller.movementSpeed;
                previousAerial = controller.aerialMovement;
                previousWeight = controller.characterWeight;
                
                controller.movementSpeed = 0;
                controller.aerialMovement = 0;
                controller.characterWeight = 0;
                
                StartCoroutine(ResumeAnimation(_damageable));
            }
        }
        
        private Damageable[] GetAlignedEnemies() {
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(controller.characterTransform.position, controller.attackAlignmentSize, 0f, Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Enemy"));
            return raycastHits.Select(raycastHit => raycastHit.transform.gameObject.GetComponent<Damageable>()).ToArray();
        }
        
        private IEnumerator ResumeAnimation(Damageable damageable) {
            yield return new WaitForSeconds(2/24f);
            controller.spriteAnimator.enabled = true;

            controller.movementSpeed = previousMovementSpeed;
            controller.aerialMovement = previousAerial;
            controller.characterWeight = previousWeight;
            damageable.resumingKnocked = true;
        }
    }
}