using System;
using Combat.CombatAttacks;
using GameFlow.EventChannels.SpecificEC;
using UI.HealthStatus;
using UnityEngine;

namespace Entities.HealthStatus.Damageables {
    public class BossDamageable : EnemyDamageable {
        [SerializeField] private EnemyStatusSO enemyStatus;
        [SerializeField] private StatusUISO statusUI;
        [SerializeField] private StatusUIChannelSO statusUIChannel;

        private float currentHealth;
        private float currentDefence;
        
        public override event DamageableCallback OnDamageableKilled;

        private void Awake() {
            if (enemyStatus == null)
                throw new DamageableException("EnemyStatus missing in BossDamageable");

            currentHealth = enemyStatus.baseHealth;
            currentDefence = enemyStatus.baseDefence;
        }

        private void Start() {
            InitializeUI();           
            UpdateUI();
        }

        public override void TakeDamage(CombatAttackSO combatAttack, Transform attackerTransform) {
            currentlyTakingAttack = combatAttack;
            currentAttackerTransform = attackerTransform;
            
            switch (currentlyTakingAttack.attackType) {
                case CombatAttackSO.AttackType.Light:
                    takingLightAttack = true;
                    break;
                case CombatAttackSO.AttackType.Heavy:
                    takingHeavyAttack = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void InflictDamage() {
            currentHealth -= currentlyTakingAttack.attackDamage / currentDefence;
            
            if (currentHealth <= 0) {
                isDead = true;
                currentHealth = 0;
                OnDamageableKilled?.Invoke(this);
                statusUIChannel.EndStatus();
            }
        }

        public override void Heal(float value) {
            currentHealth += value;
            if (currentHealth > enemyStatus.baseHealth)
                currentHealth = enemyStatus.baseHealth;
            UpdateUI();
        }

        public override void ModifyDefenseValue(float value) {
            currentDefence = enemyStatus.baseDefence + enemyStatus.baseDefence * value;
        }

        private void InitializeUI() { 
            if (statusUIChannel != null)
                statusUIChannel.InitializeStatus(statusUI);
        }

        public override void UpdateUI() {
            if (statusUIChannel != null)
                statusUIChannel.UpdateStatusHealth(currentHealth, enemyStatus.baseHealth);
        }
    }
}