using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Animator animator;
    private bool attackInProgress = false;
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.LightAttack.performed += LightAttack;
    }

    void LightAttack(InputAction.CallbackContext callbackContext) {
        if(!attackInProgress) {
            PlayerState playerState = PlayerManager.Instance.playerState;
            if(playerState == PlayerState.IDLING || playerState == PlayerState.MOVING) {
                attackInProgress = true;
                animator.SetBool("Attack", true);

                PlayerManager.Instance.SetPlayerState(PlayerState.ATTACKING);
                animator.Play("Fred Punch 1");
            }
            else if(playerState == PlayerState.ATTACKING) {
                attackInProgress = true;
                animator.SetBool("Attack", true);
            }
        }
    }

    public void AllowAttackInput() {
        attackInProgress = false;
    }
}
