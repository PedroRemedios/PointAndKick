using GameFlow.EventChannels.SpecificEC;
using UnityEngine;

namespace Combat.CombatAttacks {
    [CreateAssetMenu(fileName = "EnemySpawnMove", menuName = "Scriptable Objects/Combat/Enemy Spawn Move", order = 0)]
    public class EnemySpawnMoveSO : CombatStatusMoveSO {
        public override void PerformMove(CombatChannel combatChannel) {
            combatChannel.SpawnEntity();
        }
    }
}