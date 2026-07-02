using Combat.CombatAttacks;
using UnityEngine;

namespace Entities.Player {
    public class PlayerControllerGFX : MonoBehaviour {
        private PlayerController _playerController;

        private void Start() {
            _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }
        
        public void FinishAnimation() {
            _playerController.finishAnimation = true;
        }
        
        public void ToggleAttackInput() {
            _playerController.attackInProgress = false;
        }
        
        public void Attack(CombatAttackSO combatAttack) {
            _playerController.DealDamage(combatAttack);
        }
    }
}
