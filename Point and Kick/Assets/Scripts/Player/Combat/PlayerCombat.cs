using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerState playerState1;
    private PlayerAttack playerAttack;
    private Animator characterAnimator;
    private Animator diaboloAnimator;
    private bool attackInProgress = false;
    private bool attackModifierPressed = false;

    [SerializeField] private float totalHealth = 500;
    private float currentHealth;
    private Text textCoiso;
    private void Awake() {
        characterAnimator = GetComponent<Animator>();
        currentHealth = totalHealth;

        PlayerManager.OnPlayerStateChange += CombatOnPlayerStateChange;
    }

    private void Start() {
        diaboloAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        playerAttack = GameObject.FindWithTag("Character Attack").GetComponent<PlayerAttack>();
        textCoiso = GameObject.FindWithTag("HP2").GetComponent<Text>();

        PlayerInputActions playerInputActions = InputManager.Instance.GetPlayerInputActions();
        DefineCombatActions(playerInputActions);
    }

    private void DefineCombatActions(PlayerInputActions playerInputActions) {
        playerInputActions.Combat.LightAttack.performed += LightAttack;
        playerInputActions.Combat.HeavyAttack.performed += HeavyAttack;
        playerInputActions.Combat.DiaboloAttack.started += DiaboloAttackPressed;
        playerInputActions.Combat.DiaboloAttack.canceled += DiaboloAttackReleased;
    }

    void CombatOnPlayerStateChange(PlayerState state) {
        playerState1 = state;
        if(state != PlayerState.ATTACKING) {
            attackInProgress = false;
        }
    }
    public void Attack() {
        playerAttack.Attack();
    }

    public void TakeDamage(float damageTaken, bool heavyAttack) {
        PlayerState playerState = PlayerManager.Instance.playerState;
        if(playerState != PlayerState.DOWN && playerState != PlayerState.DEAD) {
            currentHealth -= damageTaken;
            characterAnimator.SetBool("tookDamage", true);
            diaboloAnimator.SetBool("tookDamage", true);

            if(currentHealth <= 0) {
                currentHealth = 0;
                characterAnimator.SetBool("isDead", true);
                PlayerManager.Instance.SetPlayerState(PlayerState.DEAD);

                // GetComponent<BoxCollider2D>().enabled = false;
                // Add some timer like 2 seconds to disable everything
            }
            else {
                if(!heavyAttack) {
                    characterAnimator.SetTrigger("tookLightDamage");
                    PlayerManager.Instance.SetPlayerState(PlayerState.HIT);
                }
                else {
                    characterAnimator.SetTrigger("tookHeavyDamage");
                    PlayerManager.Instance.SetPlayerState(PlayerState.DOWN);
                }
            }

            textCoiso.text = "Fred HP: " + currentHealth + " / 500";
        }
    }

    void LightAttack(InputAction.CallbackContext callbackContext) {
        if(!attackInProgress) {
            PlayerState playerState = PlayerManager.Instance.playerState;
            if(playerState == PlayerState.IDLING || playerState == PlayerState.MOVING) {
                ExecuteFirstAttack("Fred Punch 1", "Light", false);
            }
            else if(playerState == PlayerState.JUMPING) {
                // ExecuteFirstAttack("Fred Air Kick", "Light", false);
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
                    ExecuteFirstAttack("Fred Diabolo Heavy Start", "Heavy", true);
                }
                else {
                    if(characterAnimator.GetBool("Run")) {
                        ExecuteFirstAttack("Fred Headbutt", "Heavy", false);
                    }
                    else {
                        ExecuteFirstAttack("Fred Uppercut", "Heavy", false);
                    }
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
                    diaboloAnimator.SetBool("Diabolo Heavy Attack", true);
                }
            }
        }
    }

    void ExecuteFirstAttack(string attackName, string attackType, bool diaboloAttack) {
        PlayerManager.Instance.SetPlayerState(PlayerState.ATTACKING);
        attackInProgress = true;

        characterAnimator.SetInteger("Combo", 1);
        characterAnimator.Play(attackName);

        if(diaboloAttack) {
            diaboloAnimator.Play("Diabolo " + attackType + " Start");
            
            characterAnimator.SetBool("Diabolo " + attackType + " Attack", true);
            diaboloAnimator.SetBool("Diabolo " + attackType + " Attack", true);
        }
        else {
            characterAnimator.SetBool(attackType + " Attack", true);
        }
    }

    void DiaboloAttackPressed(InputAction.CallbackContext callbackContext) {
        attackModifierPressed = true;
    }

    void DiaboloAttackReleased(InputAction.CallbackContext callbackContext) {
        attackModifierPressed = false;
    }


    public void ToggleAttackInput() {
        attackInProgress = false;
    }

    public void BackToNormal() {
        PlayerManager.Instance.SetPlayerState(PlayerState.IDLING);
        characterAnimator.SetBool("tookDamage", false);
    }
}
