using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{  
    private PlayerInputActions playerInputActions;
    private PlayerController playerController;
    private BoxCollider2D groundCollider;
    private Animator animator;

    private float horizontalInput;
    private float verticalInput;
    private Vector2 moveDirection;

    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 9f;
    private float currentSpeed;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float gravityScale = 5f;

    private enum MovementDirection {UP, DOWN, LEFT, RIGHT, NONE};
    private MovementDirection previousMoveTap = MovementDirection.NONE;
    private bool preRunning = false;
    private bool isRunning = false;
    private bool isMoving = false;
    private float runInputTime = Mathf.NegativeInfinity; 

    void Awake() {
        groundCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        currentSpeed = walkSpeed;
        
        PlayerManager.OnPlayerStateChange += MovementOnPlayerStateChange;
    }

    void Start() {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        playerInputActions = InputManager.Instance.GetPlayerInputActions();
        DefineExplorationMovementActions();
        DefineCombatMovementActions();
    }

    void Update() {
        Move();
        CheckGround();
    }

    void FixedUpdate() {
        playerController.Move(moveDirection, currentSpeed);
        playerController.Jump(transform);
    }

    private void DefineExplorationMovementActions() {
        playerInputActions.Exploration.Jump.performed += Jump;
        playerInputActions.Exploration.Move.performed += DoubleTapRunStart;
        playerInputActions.Exploration.Move.canceled += DoubleTapRunStop; 

        // playerInputActions.Combat.Run.performed += RunStart;
        // playerInputActions.Combat.Run.canceled += RunStop;
    }

    private void DefineCombatMovementActions() {
        playerInputActions.Combat.Jump.performed += Jump;
        playerInputActions.Combat.Move.performed += DoubleTapRunStart;
        playerInputActions.Combat.Move.canceled += DoubleTapRunStop;
        playerInputActions.Combat.GuardDodge.started += GuardAndDodge;

        // playerInputActions.Combat.Run.performed += RunStart;
        // playerInputActions.Combat.Run.canceled += RunStop;
    }

    void Move() {
        if(GameManager.Instance.gameState == GameState.EXPLORATION || GameManager.Instance.gameState == GameState.COMBAT) {
            PlayerState playerState = PlayerManager.Instance.playerState;
            if(playerState == PlayerState.IDLING || playerState == PlayerState.MOVING || playerState == PlayerState.JUMPING) {
                moveDirection = playerInputActions.Exploration.enabled ? playerInputActions.Exploration.Move.ReadValue<Vector2>() : playerInputActions.Combat.Move.ReadValue<Vector2>();
                playerController.CheckDirection(moveDirection.x);

                float movementSpeed = moveDirection.magnitude;
                animator.SetFloat("Speed", movementSpeed);
                CheckPlayerMovementState(movementSpeed);

                animator.SetBool("Run", isRunning);
                currentSpeed = isRunning ? runSpeed : walkSpeed;
                
            }
        }
    }

	/// <summary>
	/// Checks if the Character touches the Ground Check object while jumping/landing. 
    /// If so, calls the PlayerController to stop the jump and stops the jump animation.
	/// </summary>
    void CheckGround() {
        PlayerState playerState = PlayerManager.Instance.playerState;
        if(playerState == PlayerState.JUMPING || playerState == PlayerState.LANDING) {
            RaycastHit2D raycastHit = Physics2D.Raycast(groundCollider.bounds.center, Vector2.down, groundCollider.bounds.extents.y + 0.05f, 1 << LayerMask.NameToLayer("Ground Check"));

            if(raycastHit.collider) {
                bool stoppedJump = playerController.StopJump();
                if(stoppedJump) {
                    animator.SetBool("Jump", false);
                }
            }
        }
    }

    // TODO: Hardcoded the speed = 0.1 that was written in the character animator. Getting it from the animator was shit, so idk if it's better this way.
    private void CheckPlayerMovementState(float movementSpeed) {
        PlayerState playerState = PlayerManager.Instance.playerState;
        if(playerState == PlayerState.IDLING && IsPlayerMoving(movementSpeed)) {
            PlayerManager.Instance.SetPlayerState(PlayerState.MOVING);
        }
        else if(playerState == PlayerState.MOVING && !IsPlayerMoving(movementSpeed)) {
            PlayerManager.Instance.SetPlayerState(PlayerState.IDLING);
        }
    }

    private bool IsPlayerMoving(float movementSpeed) {
        return movementSpeed >= 0.1 ? true : false;
    }

	/// <summary>
	/// Executed when the Jump Input is pressed (Spacebar - Keyboard, Button South - Gamepad).
	/// </summary>
    void Jump(InputAction.CallbackContext callbackContext) {
        if(GameManager.Instance.gameState == GameState.EXPLORATION || GameManager.Instance.gameState == GameState.COMBAT) {
            PlayerState playerState = PlayerManager.Instance.playerState;
            if(playerState == PlayerState.IDLING || playerState == PlayerState.MOVING) {
                animator.SetBool("Jump", true);
                playerController.BeginJump(gravityScale, jumpPower);
                PlayerManager.Instance.SetPlayerState(PlayerState.JUMPING);
            }
        }
    }

	/// <summary>
	/// Executed when the Run Input is pressed (Shift - Keyboard, LB - Gamepad).
	/// </summary>
    void RunStart(InputAction.CallbackContext callbackContext) {
        PlayerState playerState = PlayerManager.Instance.playerState;
        if(playerState == PlayerState.IDLING || playerState == PlayerState.MOVING) {
            isRunning = true;
        }
    }

	/// <summary>
	/// Executed when the Run Input is released (Shift - Keyboard, LB - Gamepad).
	/// </summary>
    void RunStop(InputAction.CallbackContext callbackContext) {
        PlayerState playerState = PlayerManager.Instance.playerState;
        if(playerState == PlayerState.IDLING || playerState == PlayerState.MOVING) {
            isRunning = false;
        }
    }

    void DoubleTapRunStart(InputAction.CallbackContext callbackContext) {
        MovementDirection moveDirection = CheckCharacterDirection(callbackContext.ReadValue<Vector2>());
        if(isMoving) {
            // Do Nothing
        }
        else if((!preRunning && !isRunning) || Time.time - runInputTime > 0.2) {
            runInputTime = Time.time;
            preRunning = true;
            isRunning = false;
        }
        else if(Time.time - runInputTime <= 0.2) {
            if(preRunning && previousMoveTap == moveDirection) {
                preRunning = false;
                isRunning = true;
            }
            else if(preRunning && previousMoveTap != moveDirection) {
                runInputTime = Time.time;
            }
        }

        previousMoveTap = CheckCharacterDirection(callbackContext.ReadValue<Vector2>());
        isMoving = true;
    }

    void DoubleTapRunStop(InputAction.CallbackContext callbackContext) {
        isMoving = false;

        if(preRunning && Time.time - runInputTime > 0.2) {
            preRunning = false;
        }
        else {
            runInputTime = Time.time;
        }
    }

    MovementDirection CheckCharacterDirection(Vector2 movementDirection) {
        if(Mathf.Abs(movementDirection.x) == Mathf.Abs(movementDirection.y)) {
            return MovementDirection.NONE;
        }
        else if(movementDirection.x > movementDirection.y) {
            if(movementDirection.x > 0) {
                return MovementDirection.RIGHT;
            }
            else if(movementDirection.y < 0) {
                return MovementDirection.DOWN;
            }
        }
        else if(movementDirection.x < movementDirection.y) {
            if(movementDirection.x < 0) {
                return MovementDirection.LEFT;
            }
            else if(movementDirection.y > 0) {
                return MovementDirection.UP;
            }
        }
        return MovementDirection.NONE;
    }

    void GuardAndDodge(InputAction.CallbackContext callbackContext) {
        if(GameManager.Instance.gameState == GameState.COMBAT) {
            PlayerState playerState = PlayerManager.Instance.playerState;
            if(playerState == PlayerState.IDLING || playerState == PlayerState.MOVING) {
                // If the Player is moving - GUARD
                if(IsPlayerMoving(moveDirection.magnitude)) {
                    PlayerManager.Instance.SetPlayerState(PlayerState.DODGING);
                    animator.SetTrigger("Dodge");
                }
                else {
                }
            }
        }

    }

	/// <summary>
	/// Changes the PlayerState from Landing to Idle after the landing animation ends. 
    /// Called in the last frame of the landing animation.
	/// </summary>
    private void FinishMove() {
        PlayerManager.Instance.SetPlayerState(PlayerState.IDLING);
    }

    /// <summary>
	/// Executes everytime the PlayerState changes.
	/// </summary>
	/// <param name="state">New PlayerState</param>
    void MovementOnPlayerStateChange(PlayerState state) {
        if(state == PlayerState.DEAD || state == PlayerState.HIT || state == PlayerState.DOWN || state == PlayerState.LANDING || state == PlayerState.ATTACKING) {
            moveDirection = new Vector2(0, 0);
        }
    } 
}
