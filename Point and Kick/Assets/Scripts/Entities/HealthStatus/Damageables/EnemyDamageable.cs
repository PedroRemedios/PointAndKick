using System;

namespace Entities.HealthStatus.Damageables {
    public abstract class EnemyDamageable : Damageable {
        public delegate void DamageableCallback(EnemyDamageable damageable);
        public abstract event DamageableCallback OnDamageableKilled;
    }
}