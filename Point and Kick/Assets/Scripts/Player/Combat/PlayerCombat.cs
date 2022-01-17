using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Animator characterAnimator;
    private Animator diaboloAnimator;
    private bool attackInProgress = false;
    private bool attackModifierPressed = false;
    void Awake()
    {
        characterAnimator = GetComponent<Animator>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.LightAttack.performed += LightAttack;
        playerInputActions.Player.HeavyAttack.performed += HeavyAttack;
        playerInputActions.Player.DiaboloAttack.started += DiaboloAttackPressed;
        playerInputActions.Player.DiaboloAttack.canceled += DiaboloAttackReleased;

        PlayerManager.OnPlayerStateChange += CombatOnPlayerStateChange;
    }

    void Start() {
        diaboloAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    void CombatOnPlayerStateChange(PlayerState state) {
        if(state != PlayerState.ATTACKING) {
            attackInProgress = false;
        }
    }

    void LightAttack(InputAction.CallbackContext callbackContext) {
        if(!attackInProgress) {
            PlayerState playerState = PlayerManager.Instance.playerState;
            if(playerState == PlayerState.IDLING || playerState == PlayerState.MOVING) {
                attackInProgress = true;
                PlayerManager.Instance.SetPlayerState(PlayerState.ATTACKING);

                characterAnimator.SetBool("Light Attack", true);
                characterAnimator.SetInteger("Combo", 1);
                characterAnimator.Play("Fred Punch 1");
            }
            else if(playerState == PlayerState.ATTACKING) {
                attackInProgress = true;

                characterAnimator.SetBool("Light Attack", true);
                characterAnimator.SetInteger("Combo", characterAnimator.GetInteger("Combo") + 1);
            }
        }
    }

    void HeavyAttack(InputAction.CallbackContext callbackContext) {
        if(!attackInProgress) {
            PlayerState playerState = PlayerManager.Instance.playerState;
            if(playerState == PlayerState.IDLING || playerState == PlayerState.MOVING) {
                if(attackModifierPressed) {
                    attackInProgress = true;
                    PlayerManager.Instance.SetPlayerState(PlayerState.ATTACKING);

                    characterAnimator.SetBool("Diabolo Heavy Attack", true);
                    characterAnimator.SetInteger("Combo", 1);
                    characterAnimator.Play("Fred Diabolo Heavy");
                    diaboloAnimator.Play("Diabolo Heavy");
                }
            }
            else if(playerState == PlayerState.ATTACKING) {
                attackInProgress = true;
                characterAnimator.SetInteger("Combo", characterAnimator.GetInteger("Combo") + 1);

                if(!attackModifierPressed) {
                    characterAnimator.SetBool("Heavy Attack", true);
                }
                else {
                    characterAnimator.SetBool("Diabolo Heavy Attack", true);
                }
            }
        }
    }

    void DiaboloAttackPressed(InputAction.CallbackContext callbackContext) {
        attackModifierPressed = true;
    }

    void DiaboloAttackReleased(InputAction.CallbackContext callbackContext) {
        attackModifierPressed = false;
    }

    public void AllowAttackInput() {
        attackInProgress = false;
    }
}
