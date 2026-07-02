using System.Linq;
using Entities.HealthStatus.Damageables;
using UnityEngine;

namespace Combat.CombatAttacks {
    public class EnemyAttack : Attack {
        private void OnTriggerEnter2D(Collider2D col) {
            if (col.CompareTag("Character")) {
                Damageable _damageable = col.GetComponentInParent<Damageable>();
                Damageable[] _alignedDamageables = GetAlignedPlayers();

                if (_alignedDamageables.Contains(_damageable) && !hitDamageables.Contains(_damageable)) {
                    _damageable.TakeDamage(combatAttack, controller.spriteTransform);
                }
            }
        }
        
        private Damageable[] GetAlignedPlayers() {
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(controller.characterTransform.position, controller.attackAlignmentSize, 0f, Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Player"));
            return raycastHits.Select(raycastHit => raycastHit.transform.gameObject.GetComponent<Damageable>()).ToArray();
        }
    }
}