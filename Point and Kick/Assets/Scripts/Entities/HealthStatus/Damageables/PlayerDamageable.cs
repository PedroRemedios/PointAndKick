using System;
using Combat.CombatAttacks;
using GameFlow.EventChannels.SpecificEC;
using UI.HealthStatus;
using UnityEngine;

namespace Entities.HealthStatus.Damageables {
    public class PlayerDamageable : Damageable {
        [SerializeField] private PlayerStatusSO playerStatus;
        [SerializeField] private StatusUISO statusUI;
        [SerializeField] private StatusUIChannelSO statusUIChannel;

        [NonSerialized] public float blockPercentage;

        private void Awake() {
            if (playerStatus == null)
                throw new DamageableException("PlayerStatus missing in PlayerDamageable");

            playerStatus.ResetStatus(); // TODO: Placeholder before save system
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
            playerStatus.InflictDamage(currentlyTakingAttack.attackDamage * (1 - blockPercentage));

            if (playerStatus.currentHealth <= 0)
                isDead = true;
        }

        public override void Heal(float value) {
            playerStatus.RestoreHealth(value);
            UpdateUI();
        }

        public override void ModifyDefenseValue(float value) {
            playerStatus.ModifyDefenseStatus(value);
        }

        private void InitializeUI() { 
            if (statusUIChannel != null)
                statusUIChannel.InitializeStatus(statusUI);
        }

        public override void UpdateUI() {
            if (statusUIChannel != null)
                statusUIChannel.UpdateStatusHealth(playerStatus.currentHealth, playerStatus.baseHealth);
        }
    }
}