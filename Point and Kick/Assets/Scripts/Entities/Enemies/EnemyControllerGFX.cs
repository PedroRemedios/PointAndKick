using Combat.CombatAttacks;
using UnityEngine;

namespace Entities.Enemies {
    public class EnemyControllerGFX : MonoBehaviour {
        private EnemyController _enemyController;

        private void Start() {
            _enemyController = transform.parent.gameObject.GetComponent<EnemyController>();
        }
    
        private void FinishAnimation() {
            _enemyController.finishAnimation = true;
        }

        private void AttackMove(CombatAttackSO combatAttack) {
            _enemyController.DealDamage(combatAttack);
        }

        private void StatusMove(CombatStatusMoveSO combatStatusMove) {
            _enemyController.StatusMove(combatStatusMove);
        }
    }
}