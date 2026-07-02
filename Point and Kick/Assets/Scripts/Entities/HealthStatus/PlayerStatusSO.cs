using UnityEngine;

namespace Entities.HealthStatus {
    [CreateAssetMenu(fileName = "PlayerStatus", menuName = "Scriptable Objects/Combat/Player Status", order = 1)]
    public class PlayerStatusSO : ScriptableObject {
        
        public float initialHealth;
        public float initialDefence;
        
        public float baseHealth;
        public float baseDefence;

        public float currentHealth;
        public float currentDefence;

        public void ResetStatus() {
            baseHealth = initialHealth;
            baseDefence = initialDefence;

            currentHealth = baseHealth;
            currentDefence = baseDefence;
        }
        
        public void SetBaseHealth(float health) {
            baseHealth = health;
        }

        public void SetBaseDefence(float defence) {
            baseDefence = defence;
        }

        public void SetCurrentHealth(float health) {
            currentHealth = health;
        }

        public void SetCurrentDefence(float defence) {
            currentDefence = defence;
        }

        public void InflictDamage(float damage) {
            currentHealth -= damage / currentDefence;
        }

        public void ModifyDefenseStatus(float value) {
            currentDefence = baseDefence + baseDefence * value;
        }

        public void RestoreHealth(float health) {
            currentHealth += health;
            if(currentHealth > baseHealth)
                currentHealth = baseHealth;
        }
    }
}