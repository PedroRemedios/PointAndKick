using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    private enum SpawnMethod {INFINITE, ROUNDS};
    private SpawnMethod spawnMethod;

    // Rounds Mode (Not Implemented)
    public List<CombatRound> combatRounds;

    // Infinite Mode
    public GameObject infiniteEnemy;
    public int simultaneouslyAlive;
    private int currentlyAlive;

    private CinemachineConfiner cinemachineConfiner;
    private Animator cinemachineAnimator;
    private BoxCollider2D combatTrigger;
    private EdgeCollider2D combatBorder;
    private bool inCombat;

    public InputAction testExit;

    private void OnEnable() {
        testExit.Enable();
    }

    private void OnDisable() {
        testExit.Disable();
    }

    private void Awake() {
        combatTrigger = GetComponent<BoxCollider2D>();
        combatBorder = GetComponent<EdgeCollider2D>();

        testExit.performed += TestCombatExit;
    }

    void Start() {
        cinemachineConfiner = GameObject.FindWithTag("CM Camera").transform.Find("CM Combat Camera").GetComponent<CinemachineConfiner>();
        cinemachineAnimator = GameObject.FindWithTag("CM Camera").GetComponent<Animator>();
        inCombat = GameManager.Instance.gameState == GameState.COMBAT;

        if (combatRounds.Count == 0) {
            spawnMethod = SpawnMethod.INFINITE;
            currentlyAlive = 0;
        }
    }

    void Update() {
        if (inCombat) {
            if (spawnMethod == SpawnMethod.INFINITE) {
                SpawnInfiniteEnemies();
            }
        }
    }

    private void SpawnInfiniteEnemies() {
        if (currentlyAlive < simultaneouslyAlive) {
            GameObject enemy = Instantiate(infiniteEnemy, transform.position, transform.rotation);
            enemy.GetComponent<EnemyController>().onEnemyStateChange += ManageEnemyState;
            currentlyAlive++;
        }
    }

    private void ManageEnemyState(EnemyState state) {
        if (state == EnemyState.DESTROYED)
            currentlyAlive--;
    }

    private void TestCombatExit(InputAction.CallbackContext callbackContext) {
        ExitCombat();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            EnterCombat();
        }
    }

    private void EnterCombat() {
        GameManager.Instance.SetGameState(GameState.COMBAT);
        inCombat = true;

        // Change the Combat Camera confiner to the attached area
        PolygonCollider2D newCameraConfinerObject = transform.Find("Camera Confiner").GetComponent<PolygonCollider2D>();
        cinemachineConfiner.m_BoundingShape2D = newCameraConfinerObject;

        // Change to the Combat Camera and enable the area border
        cinemachineAnimator.Play("Combat Camera");
        combatBorder.enabled = true;
        combatTrigger.enabled = false;
    }

    private void ExitCombat() {
        if (inCombat) {
            GameManager.Instance.SetGameState(GameState.EXPLORATION);
            inCombat = false;

            // Change to the Normal Camera and disable the area border
            cinemachineAnimator.Play("Normal Camera");
            combatBorder.enabled = false;
            combatTrigger.enabled = true;
        }
    }

    [System.Serializable]
    public class CombatRound {
        public List<EnemyWithCount> enemies = new List<EnemyWithCount>();

        [System.Serializable]
        public class EnemyWithCount {
            public GameObject enemyObject;
            public int enemyCount;
        }
    }

}
