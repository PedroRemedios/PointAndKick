using System;
using GameFlow.EventChannels.GeneralEC;
using UnityEngine;

namespace Combat.CombatAttacks {
    [CreateAssetMenu(fileName = "CombatRound", menuName = "Scriptable Objects/Combat/Combat Round", order = 0)]
    public class CombatRoundSO : ScriptableObject {
        [Header("Detailed Enemies List")]
        public EnemySpawnInfo[] enemiesSpawnInfo;
        
        [Header("End of Combat Round Trigger")]
        public VoidEventChannelSO eventTrigger;
    }
    
    [Serializable]
    public class EnemySpawnInfo {
        public enum SpawnPositionFormat { Random, Position }

        public SpawnPositionFormat spawnFormat;
        public GameObject enemyPrefab;
        public Vector2 spawnPosition;
    }
}