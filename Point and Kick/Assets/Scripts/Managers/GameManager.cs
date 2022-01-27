using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { EXPLORATION, COMBAT, PAUSE, TRANSITION_BEGIN, TRANSITION_END }

public class GameManager : MonoBehaviour {
    protected GameManager() {}
    private static GameManager instance = null;
    public static GameManager Instance {
        get { return instance; }
    }

    public GameState gameState { get; private set; }
    public delegate void OnGameStateChangeHandler(GameState state);
    public static event OnGameStateChangeHandler OnGameStateChange;

    void Awake() {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
            Application.targetFrameRate = 200;
        }
    }

    void Start() {
        SetGameState(GameState.EXPLORATION);
    }

    public void SetGameState(GameState state){
        this.gameState = state;
        OnGameStateChange(state);
    }

    public void OnDestroy(){
        GameManager.instance = null;
    }
}
