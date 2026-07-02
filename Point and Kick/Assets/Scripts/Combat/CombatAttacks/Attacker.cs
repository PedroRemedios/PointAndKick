using UnityEngine;

namespace Combat.CombatAttacks {
    public class Attacker : MonoBehaviour {
        [SerializeField] private Attack characterAttack;
        [SerializeField] private Attack weaponAttack;
        
        public void EnableCharacterAttack(CombatAttackSO combatAttack) {
            characterAttack.SetupAttack(combatAttack);
            characterAttack.gameObject.SetActive(true);
        }

        public void DisableCharacterAttack() {
            characterAttack.gameObject.SetActive(false);
        }

        public void EnableWeaponAttack(CombatAttackSO combatAttack) {
            weaponAttack.SetupAttack(combatAttack);
            weaponAttack.gameObject.SetActive(true);
        }

        public void DisableWeaponAttack() {
            weaponAttack.gameObject.SetActive(false);
        }
    }
}