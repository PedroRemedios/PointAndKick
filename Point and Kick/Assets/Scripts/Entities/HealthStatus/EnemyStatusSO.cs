using UnityEngine;

namespace Entities.HealthStatus {
    [CreateAssetMenu(fileName = "EnemyStatus", menuName = "Scriptable Objects/Combat/Enemy Status", order = 2)]
    public class EnemyStatusSO : ScriptableObject {
        public float baseHealth;
        public float baseDefence;
    }
}