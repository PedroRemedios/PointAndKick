using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { PLAY, PAUSE, TRANSITION_BEGIN, TRANSITION_END }

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
        }
    }

    void Start() {
        SetGameState(GameState.PLAY);
    }

    public void SetGameState(GameState state){
        this.gameState = state;
        // OnStateChange(state);
    }

    public void OnDestroy(){
        GameManager.instance = null;
    }
}
