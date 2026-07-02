using System.Collections.Generic;
using Entities;
using Entities.HealthStatus.Damageables;
using UnityEngine;

namespace Combat.CombatAttacks {
    public abstract class Attack : MonoBehaviour {
        [SerializeField] protected Controller controller;

        protected CombatAttackSO combatAttack;
        protected List<Damageable> hitDamageables = new List<Damageable>();

        public void SetupAttack(CombatAttackSO attack) {
            hitDamageables.Clear();
            combatAttack = attack;
        }
    }
}