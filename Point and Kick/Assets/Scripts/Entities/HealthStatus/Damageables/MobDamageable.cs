using System;
using Combat.CombatAttacks;
using UI.HealthStatus;
using UnityEngine;

namespace Entities.HealthStatus.Damageables {
    public class MobDamageable : EnemyDamageable {
        [SerializeField] private EnemyStatusSO enemyStatus;
        [SerializeField] private HitParticlesUISO hitParticlesUI;

        private readonly Vector3 enemyHeight = new Vector3(1.5f, 3f);
        private float currentHealth;
        private float currentDefence;

        public override event DamageableCallback OnDamageableKilled;
        
        private void Awake() {
            if (enemyStatus == null)
                throw new DamageableException("EnemyStatus missing in MobDamageable");
            
            if (hitParticlesUI == null)
                throw new DamageableException("HitParticlesUI missing in MobDamageable");

            currentHealth = enemyStatus.baseHealth;
            currentDefence = enemyStatus.baseDefence;
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

            if (currentHealth > 0) return;
            switch (currentlyTakingAttack.attackType) {
                case CombatAttackSO.AttackType.Light:
                    currentHealth += currentlyTakingAttack.attackDamage / currentDefence;
                    break;
                case CombatAttackSO.AttackType.Heavy:
                    isDead = true;
                    currentHealth = 0;
                    OnDamageableKilled?.Invoke(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        public override void Heal(float value) {
            currentHealth += value;
            if (currentHealth > enemyStatus.baseHealth)
                currentHealth = enemyStatus.baseHealth;
        }

        public override void ModifyDefenseValue(float value) {
            currentDefence = enemyStatus.baseDefence + enemyStatus.baseDefence * value;
        }

        public override void UpdateUI() {
            if (hitParticlesUI == null) return;
            
            var enemyTransform = transform;
            Vector3 particleOffset = enemyHeight;
            particleOffset.x *= currentAttackerTransform.lossyScale.x;
            particleOffset.y *= currentAttackerTransform.lossyScale.y;
            float healthPercentage = currentHealth / enemyStatus.baseHealth;
            
            GameObject particleGameObject;
            if (healthPercentage > hitParticlesUI.greenLowerTrigger)
                particleGameObject = Instantiate(hitParticlesUI.greenParticle, enemyTransform.position + particleOffset, enemyTransform.rotation);
            else if (healthPercentage > hitParticlesUI.yellowLowerTrigger)
                particleGameObject = Instantiate(hitParticlesUI.yellowParticle, enemyTransform.position + particleOffset, enemyTransform.rotation);
            else if (healthPercentage > hitParticlesUI.orangeLowerTrigger)
                particleGameObject = Instantiate(hitParticlesUI.orangeParticle, enemyTransform.position + particleOffset, enemyTransform.rotation);
            else if (healthPercentage > hitParticlesUI.redLowerTrigger)
                particleGameObject = Instantiate(hitParticlesUI.redParticle, enemyTransform.position + particleOffset, enemyTransform.rotation);
            else
                particleGameObject = Instantiate(hitParticlesUI.brokenParticle, enemyTransform.position + particleOffset, enemyTransform.rotation);
            
            HitParticlesUI hitParticlesScript = particleGameObject.GetComponent<HitParticlesUI>();
            hitParticlesScript.Move(currentAttackerTransform.lossyScale.x);
        }
    }
}