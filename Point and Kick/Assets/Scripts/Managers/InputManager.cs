using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    protected InputManager() {}
    private static InputManager instance = null;
    public static InputManager Instance { get { return instance; } }

    private PlayerInputActions playerInputActions;

    private void Awake() {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
        
        playerInputActions = new PlayerInputActions();
        playerInputActions.Exploration.Enable(); // Eventually the Game will start in the UI Menu, so when the time comes, change this here

        GameManager.OnGameStateChange += GameStateInput;
    }

    private void GameStateInput(GameState gameState) {
        DisableCurrentInputMap();
        EnableNewInputMap(gameState);
    }

    private void DisableCurrentInputMap() {
        if (playerInputActions.Exploration.enabled) playerInputActions.Exploration.Disable();
        else if (playerInputActions.Combat.enabled) playerInputActions.Combat.Disable();
        else if (playerInputActions.UI.enabled) playerInputActions.UI.Disable();
    }

    private void EnableNewInputMap(GameState gameState) {
        if (gameState == GameState.EXPLORATION) playerInputActions.Exploration.Enable();
        else if (gameState == GameState.COMBAT) playerInputActions.Combat.Enable();
        else if (gameState == GameState.PAUSE) playerInputActions.UI.Enable();
    }

    public PlayerInputActions GetPlayerInputActions() {
        return playerInputActions;
    }
}
