using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { EXPLORATION, INTERACTION, COMBAT, PAUSE, TRANSITION_BEGIN, TRANSITION_END, QTE }

public class GameManager : MonoBehaviour {
    protected GameManager() {}
    private static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }

    public GameState gameState { get; private set; }
    public delegate void OnGameStateChangeHandler(GameState state);
    public static event OnGameStateChangeHandler OnGameStateChange;

    private void Awake() {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else {
            instance = this;
            Application.targetFrameRate = 200;
        }
    }

    private void Start() {
        SetGameState(GameState.EXPLORATION); //If you want to use a Scene not related to the QTE, change this line to SetGameState(GameState.Exploration);
    }

    public void SetGameState(GameState state){
        this.gameState = state;
        OnGameStateChange(state);
    }

    public void OnDestroy() {
        GameManager.instance = null;
    }
}
