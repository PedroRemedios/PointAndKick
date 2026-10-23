using System;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine.ScriptableObjects;

namespace Combat.CombatAttacks {
    [CreateAssetMenu(fileName = "CombatMoveset", menuName = "Scriptable Objects/Combat/Combat Moveset", order = 1)]
    public class CombatMovesetSO : ScriptableObject {
        public List<CombatMove> combatMoves;
    }
    
    [Serializable]
    public class CombatMove {
        public StateSO attackState;
        public float attackChance;
    }
}