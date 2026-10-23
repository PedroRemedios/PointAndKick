using GameFlow.EventChannels.SpecificEC;
using UnityEngine;

namespace Combat.CombatAttacks {
    public abstract class CombatStatusMoveSO : ScriptableObject {
        public abstract void PerformMove(CombatChannel combatChannel);
    }
}