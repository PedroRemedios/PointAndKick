using System;
using System.Collections.Generic;
using System.Linq;
using Combat.CombatAttacks;
using Combat.CombatElements;
using Entities;
using Entities.Enemies;
using Entities.HealthStatus.Damageables;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Combat {
    public class CombatManager : MonoBehaviour {
        [SerializeField] private CombatChannel combatChannel;
        [SerializeField] private Animator cameraAnimator;
        [FormerlySerializedAs("camera")] [SerializeField] private Camera mainCamera;

        private CombatSO currentCombat;
        private CombatRoundSO currentRound;
        private int roundIndex;
        
        private Transform cameraTransform;
        private float halfCameraWidth;
        private Transform playerTransform;

        private GameObject leftLockEnemy;
        private GameObject rightLockEnemy;
        
        private readonly List<GameObject> aliveEnemies = new List<GameObject>();
        private readonly Vector2 sideDistance = new Vector2(1.6f, 0);

        private void Awake() {
            cameraTransform = mainCamera.transform;
            halfCameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        }

        private void Start() {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void OnEnable() {
            combatChannel.OnCombatDataSent += EnterCombat;
            combatChannel.OnEnemySpawn += SpawnEnemy;
            combatChannel.OnEnemyStatusChanged += ApplyEffectToEnemy;
            combatChannel.OnCheckEnemies += CheckEnemyCount;
            combatChannel.OnClosestEnemyRequested += GetClosestEnemy;
            combatChannel.OnLockPlayerSide += TryLockPlayerSide;
            combatChannel.OnEnemyLockedCheck += CheckEnemyOnLock;
            combatChannel.OnEnemyLockRemove += RemovePlayerLocks;
        }

        private PlayerLockSide TryLockPlayerSide(GameObject enemy, bool force) {
            Vector2 enemyPosition = enemy.transform.position;
            Vector2 playerPosition = playerTransform.position;
            
            float leftDistance = Vector2.Distance(enemyPosition, playerPosition - sideDistance);
            float rightDistance = Vector2.Distance(enemyPosition, playerPosition + sideDistance);
            
            if (leftDistance < rightDistance) {
                if (leftLockEnemy == null || leftLockEnemy == enemy || force) {
                    if (rightLockEnemy == enemy)
                        rightLockEnemy = null;
                    
                    leftLockEnemy = enemy;
                    return PlayerLockSide.Left;
                }
                if (rightLockEnemy == null || rightLockEnemy == enemy) {
                    if (leftLockEnemy == enemy)
                        leftLockEnemy = null;
                    
                    rightLockEnemy = enemy;
                    return PlayerLockSide.Right;
                }

                return PlayerLockSide.None;
            }
            else {
                if (rightLockEnemy == null || rightLockEnemy == enemy || force) {
                    if (leftLockEnemy == enemy)
                        leftLockEnemy = null;
                    
                    rightLockEnemy = enemy;
                    return PlayerLockSide.Right;
                }
                if (leftLockEnemy == null || leftLockEnemy == enemy) {
                    if (rightLockEnemy == enemy)
                        rightLockEnemy = null;
                    
                    leftLockEnemy = enemy;
                    return PlayerLockSide.Left;
                }
            
                return PlayerLockSide.None;
            }
        }

        private bool CheckEnemyOnLock(GameObject enemy) {
            return leftLockEnemy == enemy || rightLockEnemy == enemy;
        }

        private bool RemovePlayerLocks(GameObject enemy) {
            if (leftLockEnemy == enemy)
                leftLockEnemy = null;
            if (rightLockEnemy == enemy)
                rightLockEnemy = null;
            return true;
        }

        private void OnDisable() {
            combatChannel.OnCombatDataSent -= EnterCombat;
            combatChannel.OnEnemySpawn -= SpawnEnemy;
            combatChannel.OnEnemyStatusChanged -= ApplyEffectToEnemy;
            combatChannel.OnCheckEnemies -= CheckEnemyCount;
            combatChannel.OnClosestEnemyRequested -= GetClosestEnemy;
            combatChannel.OnLockPlayerSide -= TryLockPlayerSide;
            combatChannel.OnEnemyLockedCheck -= CheckEnemyOnLock;
            combatChannel.OnEnemyLockRemove -= RemovePlayerLocks;
        }

        private bool CheckEnemyCount(int value) {
            return aliveEnemies.Count >= value;
        }

        private Transform GetClosestEnemy(Transform requesterPosition, float visionRange) {
            Transform closestEnemy = null;
            float closestDistance = Mathf.Infinity;            
            
            foreach (var enemy in aliveEnemies) {
                float distance = Vector2.Distance(requesterPosition.position, enemy.transform.position);
                if (distance < 0.01f)
                    continue;
                
                if (distance < visionRange && distance < closestDistance) {
                    closestEnemy = enemy.transform;
                    closestDistance = distance;
                }
            }

            return closestEnemy;
        }

        private void EnterCombat(CombatSO combatData) {
            GameManager.Instance.SetGameState(GameState.COMBAT);
            
            roundIndex = 0;
            currentCombat = combatData;
            StartRound();

            cameraAnimator.Play("Combat Camera");
            combatChannel.StartCombat();
        }

        private void StartRound() {
            if (roundIndex >= currentCombat.combatRounds.Length)
                EndCombat();
            else {
                CombatRoundSO combatRound = currentCombat.combatRounds[roundIndex];
                currentRound = combatRound;
                SpawnEnemies();
            }
        }

        private void SpawnEnemy() {
            if (aliveEnemies.Count >= currentCombat.enemyCap)
                return;
            
            //Vector3 cameraPosition = cameraTransform.position;
            float enemySide = Random.Range(-1f, 1f);
            Vector2 spawnPosition = combatChannel.GenerateRandomPosition(new Vector2(enemySide, 0));
            //Vector3 spawnPosition = GenerateSpawnPosition(new Vector2(-100, 0), cameraPosition);

            float enemyValue = Random.Range(0f, 1f);
            float enemyChance = 0;
        
            foreach (CombatSO.RandomEnemySpawnChance enemySpawn in currentCombat.spawnableEnemies) {
                enemyChance += enemySpawn.spawnChance;
                if (enemyValue < enemyChance) {
                    GameObject enemyGameObject = Instantiate(enemySpawn.enemyPrefab, spawnPosition, cameraTransform.rotation);
                    aliveEnemies.Add(enemyGameObject);
                
                    EnemyDamageable enemyDamageable = enemyGameObject.GetComponent<EnemyDamageable>();
                    enemyDamageable.OnDamageableKilled += EnemyKilled;
                    return;
                }
            }
        }

        private void ApplyEffectToEnemy(EnemyStatusMoveSO.EnemyStatus enemyStatus, float statusValue) {
            int enemyIndex = Random.Range(0, aliveEnemies.Count);
            GameObject enemyToAffect = aliveEnemies[enemyIndex];
            
            Controller controller = enemyToAffect.GetComponent<Controller>();
            Damageable damageable = enemyToAffect.GetComponent<Damageable>();
            switch (enemyStatus) {
                case EnemyStatusMoveSO.EnemyStatus.Attack:
                    controller.spriteAnimator.SetTrigger("attAura");
                    // TODO: Add Attack to Controller
                    break;
                case EnemyStatusMoveSO.EnemyStatus.Defense:
                    controller.spriteAnimator.SetTrigger("defAura");
                    damageable.ModifyDefenseValue(statusValue);
                    break;
                case EnemyStatusMoveSO.EnemyStatus.LifePoints:
                    controller.spriteAnimator.SetTrigger("healAura");
                    damageable.Heal(statusValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemyStatus), enemyStatus, null);
            }
        }

        private void SpawnEnemies() {
            foreach (var enemy in currentRound.enemiesSpawnInfo) {
                Vector2 spawnPosition = Vector2.zero;
                
                switch (enemy.spawnFormat) {
                    case EnemySpawnInfo.SpawnPositionFormat.Random:
                        spawnPosition = combatChannel.GenerateRandomPosition(enemy.spawnPosition);
                        break;
                    case EnemySpawnInfo.SpawnPositionFormat.Position:
                        spawnPosition = combatChannel.GenerateSpecificPosition(enemy.spawnPosition);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                // Rotation is not Important, since they begin to the left by default, and the random ones are out of camera
                GameObject enemyGameObject = Instantiate(enemy.enemyPrefab, spawnPosition, cameraTransform.rotation);
                aliveEnemies.Add(enemyGameObject);
                
                if (enemy.spawnFormat == EnemySpawnInfo.SpawnPositionFormat.Position) {
                    EnemyController enemyController = enemyGameObject.GetComponent<EnemyController>();
                    enemyController.startedCombat = true;

                    CapsuleCollider2D enemyCollider = enemyGameObject.GetComponent<CapsuleCollider2D>();
                    enemyCollider.isTrigger = false;
                }
                
                EnemyDamageable enemyDamageable = enemyGameObject.GetComponent<EnemyDamageable>();
                enemyDamageable.OnDamageableKilled += EnemyKilled;
            }
        }

        /*private Vector2 GenerateSpawnPosition(float spawnSide) {
            if (spawnSide < 0)
                return combatChannel.GenerateLeftPosition();
            else
                return combatChannel.GenerateRightPosition();


            Vector2 spawnPosition = Vector2.zero;
            
            if (enemyPosition.x <= -100)
                spawnPosition.x = cameraPosition.x - (halfCameraWidth + 1);
            else if (enemyPosition.x >= 100)
                spawnPosition.x = cameraPosition.x + (halfCameraWidth + 1);
            else
                spawnPosition.x = cameraPosition.x + enemyPosition.x;
            
            spawnPosition.y = cameraPosition.y + enemyPosition.y;
            return spawnPosition;
        }*/

        private void EnemyKilled(EnemyDamageable damageableEnemy) {
            aliveEnemies.Remove(damageableEnemy.gameObject);
            damageableEnemy.OnDamageableKilled -= EnemyKilled;
            
            if (aliveEnemies.Count <= 0)
                EndRound();
        }

        private void EndRound() {
            currentRound = null;
            roundIndex++;
            if (roundIndex >= currentCombat.combatRounds.Length)
                EndCombat();
            else {
                StartRound();
            }
        }

        private void EndCombat() {
            GameManager.Instance.SetGameState(GameState.EXPLORATION);
            currentCombat = null;

            cameraAnimator.Play("Normal Camera");
        }
        
        /*
         * ENEMY FOLLOW LOCKING SYSTEM
         */
        
    }
}