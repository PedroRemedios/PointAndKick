using System;
using Combat.CombatAttacks;
using UnityEngine;

namespace Combat.CombatElements {
    [CreateAssetMenu(fileName = "Combat", menuName = "Scriptable Objects/Combat/Combat", order = 0)]
    public class CombatSO : ScriptableObject {
        [Header("Combat Rounds")]
        public CombatRoundSO[] combatRounds;

        [Header("Maximum Number of Enemies")] 
        public int enemyCap;
        
        [Header("Spawnable Enemies")] 
        public RandomEnemySpawnChance[] spawnableEnemies;
    
        [Serializable]
        public class RandomEnemySpawnChance {
            public GameObject enemyPrefab;
            public float spawnChance;
        }
    }
}