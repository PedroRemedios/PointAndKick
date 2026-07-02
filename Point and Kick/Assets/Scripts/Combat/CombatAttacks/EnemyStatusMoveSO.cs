using GameFlow.EventChannels.SpecificEC;
using UnityEngine;

namespace Combat.CombatAttacks {
    [CreateAssetMenu(fileName = "EnemyStatusMove", menuName = "Scriptable Objects/Combat/Enemy Status Move", order = 0)]
    public class EnemyStatusMoveSO : CombatStatusMoveSO {
        public enum EnemyStatus { Attack, Defense, LifePoints }

        public EnemyStatus enemyStatus;
        public float statusValue;

        public override void PerformMove(CombatChannel combatChannel) {
            combatChannel.ChangeEnemyStatus(enemyStatus, statusValue);
        }
    }
}