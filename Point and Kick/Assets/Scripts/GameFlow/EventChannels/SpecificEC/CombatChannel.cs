using System;
using Combat;
using Combat.CombatAttacks;
using Combat.CombatElements;
using UnityEngine;

namespace GameFlow.EventChannels.SpecificEC {
    [CreateAssetMenu(fileName = "CombatChannel", menuName = "Events/Combat Channel", order = 8)]
    public class CombatChannel : ScriptableObject {
        public delegate void VoidCallback();
        public event VoidCallback OnEnemySpawn;
        public event VoidCallback OnCombatStarted;
        public event VoidCallback OnCombatEnded;
        public event VoidCallback OnBorderAnimation;
        public void StartCombat() {
            OnCombatStarted?.Invoke();
        }
        public void EndCombat() {
            OnCombatEnded?.Invoke();
        }
        public void SpawnEntity() {
            OnEnemySpawn?.Invoke();
        }        
        public void ToggleBorderAnimation() {
            OnBorderAnimation?.Invoke();
        }

        public delegate bool EnemyCountCallback(int value);
        public event EnemyCountCallback OnCheckEnemies;
        public bool IsEnemyCount(int value) {
            return OnCheckEnemies != null && OnCheckEnemies.Invoke(value);
        }
        
        public delegate Transform ClosestEnemyCallback(Transform requesterTransform, float range);
        public event ClosestEnemyCallback OnClosestEnemyRequested;
        public Transform GetClosestVisibleEnemy(Transform requesterTransform, float range) {
            return OnClosestEnemyRequested?.Invoke(requesterTransform, range);
        }
        
        
        public delegate Vector2 SpawnPositionCallback(Vector2 position);
        public event SpawnPositionCallback OnSpawnSpecificPositionRequested;
        public event SpawnPositionCallback OnSpawnRandomPositionRequested;
        public Vector2 GenerateRandomPosition(Vector2 position) {
            return OnSpawnRandomPositionRequested?.Invoke(position) ?? Vector2.negativeInfinity;
        }
        public Vector2 GenerateSpecificPosition(Vector2 position) {
            return OnSpawnSpecificPositionRequested?.Invoke(position) ?? Vector2.negativeInfinity;
        }
        
        public delegate void EnemyStatusCallback(EnemyStatusMoveSO.EnemyStatus enemyStatus, float statusValue);
        public event EnemyStatusCallback OnEnemyStatusChanged;
        
        public delegate void CombatDataCallback(CombatSO combatData);
        public event CombatDataCallback OnCombatDataSent;
        

        public void SendCombatData(CombatSO combatData) {
            OnCombatDataSent?.Invoke(combatData);
        }


        public void ChangeEnemyStatus(EnemyStatusMoveSO.EnemyStatus enemyStatus, float statusValue) {
            OnEnemyStatusChanged?.Invoke(enemyStatus, statusValue);
        }
        
        /*
         * ENEMY FOLLOW LOCKING SYSTEM
         */
        public delegate PlayerLockSide FollowLockPriorityCallback(GameObject enemy, bool force);
        public event FollowLockPriorityCallback OnLockPlayerSide;
        
        public PlayerLockSide LockPlayerSide(GameObject enemy, bool force) {
            return OnLockPlayerSide != null ? OnLockPlayerSide.Invoke(enemy, force) : PlayerLockSide.None;
        }


        public delegate bool FollowLockCallback(GameObject enemy);
        public event FollowLockCallback OnEnemyLockedCheck;
        public event FollowLockCallback OnEnemyLockRemove;
        public bool CheckEnemyOnLock(GameObject enemy) {
            return OnEnemyLockedCheck != null && OnEnemyLockedCheck.Invoke(enemy);
        }
        public bool RemoveEnemyOnLock(GameObject enemy) {
            return OnEnemyLockRemove != null && OnEnemyLockRemove.Invoke(enemy);
        }
        
        
    }
}