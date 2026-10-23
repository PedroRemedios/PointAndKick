using UnityEngine;

namespace Combat.CombatAttacks {
    [CreateAssetMenu(fileName = "CombatAttack", menuName = "Scriptable Objects/Combat/Combat Attack", order = 0)]
    public class CombatAttackSO : ScriptableObject {
        public enum AttackType {Light, Heavy}

        [Header("Attack Type")] 
        public AttackType attackType;

        [Header("Attack Base Damage")] 
        public float attackDamage;

        [Header("Attack Collider Area")] 
        public Vector2 attackPositionOffset;
        public Vector2 attackColliderArea;

        [Header("Knock-back Force")] 
        public Vector2 knockbackForce;

        [Header("Hit Particle")] 
        public GameObject hitParticle;
        public Vector2 hitParticleOffset;
    }
}