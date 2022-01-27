using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { IDLING, MOVING, JUMPING, LANDING, ATTACKING, DODGING, HIT, DOWN, DEAD }

public class PlayerManager : MonoBehaviour
{
    protected PlayerManager() {}
    private static PlayerManager instance = null;
    public static PlayerManager Instance {
        get { return instance; }
    }

    public PlayerState playerState { get; private set; }
    public delegate void OnPlayerStateChangeHandler(PlayerState state);
    public static event OnPlayerStateChangeHandler OnPlayerStateChange;

    void Awake() {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    void Start() {
        SetPlayerState(PlayerState.IDLING);
    }

    public void SetPlayerState(PlayerState state){
        this.playerState = state;
        OnPlayerStateChange(state);
    }

    public void OnDestroy(){
        PlayerManager.instance = null;
    }
}
