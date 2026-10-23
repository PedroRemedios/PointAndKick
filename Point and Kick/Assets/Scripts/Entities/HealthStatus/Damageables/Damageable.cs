using System;
using Combat.CombatAttacks;
using UnityEngine;

namespace Entities.HealthStatus.Damageables {
    public abstract class Damageable : MonoBehaviour {
        // State Machine Condition Variables
        [NonSerialized] public bool takingLightAttack;
        [NonSerialized] public bool takingHeavyAttack;
        [NonSerialized] public bool resumingKnocked;
        [NonSerialized] public bool isDead;
        [NonSerialized] public bool isStunned;
        
        // Attacker Information
        [NonSerialized] public CombatAttackSO currentlyTakingAttack;
        [NonSerialized] public Transform currentAttackerTransform;
        
        public abstract void TakeDamage(CombatAttackSO combatAttack, Transform attackerTransform);
        public abstract void InflictDamage();
        public abstract void Heal(float value);
        public abstract void ModifyDefenseValue(float value);
        
        public abstract void UpdateUI();
    }
    
    public class DamageableException : Exception {
        public DamageableException(string message) : base(message) { }
    }
}