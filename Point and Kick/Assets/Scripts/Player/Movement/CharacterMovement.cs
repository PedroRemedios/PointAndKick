using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{  
    private PlayerInputActions playerInputActions;
    private PlayerController playerController;
    private Animator animator;

    private float horizontalInput;
    private float verticalInput;
    private Vector2 moveDirection;

    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 9f;
    private float currentSpeed;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float gravityScale = 5f;

    void Awake() {
        Application.targetFrameRate = 200;
        currentSpeed = walkSpeed;

        animator = GetComponent<Animator>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Run.started += RunStart;
        playerInputActions.Player.Run.canceled += RunStop;
        PlayerManager.OnPlayerStateChange += MovementOnPlayerStateChange;
    }

    void Start() {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update() {
        Move();
    }

    void FixedUpdate() {
        playerController.Move(moveDirection, currentSpeed);
        playerController.Jump(transform);
    }

    void Move() {
        if(GameManager.Instance.gameState == GameState.PLAY) {
            PlayerState playerState = PlayerManager.Instance.playerState;
            if(playerState == PlayerState.IDLING || playerState == PlayerState.MOVING || playerState == PlayerState.JUMPING) {
                moveDirection = playerInputActions.Player.Move.ReadValue<Vector2>();
                playerController.CheckDirection(moveDirection.x);

                float movementSpeed = moveDirection.magnitude;
                animator.SetFloat("Speed", movementSpeed);
                CheckPlayerMovementState(movementSpeed);
            }
        }
    }

    // TODO: Hardcoded the speed = 0.1 that was written in the character animator. Getting it from the animator was shit, so idk if it's better this way.
    private void CheckPlayerMovementState(float movementSpeed) {
        PlayerState playerState = PlayerManager.Instance.playerState;
        if(playerState == PlayerState.IDLING && movementSpeed >= 0.1) {
            PlayerManager.Instance.SetPlayerState(PlayerState.MOVING);
        }
        else if(playerState == PlayerState.MOVING && movementSpeed < 0.1) {
            PlayerManager.Instance.SetPlayerState(PlayerState.IDLING);
        }
    }

    void Jump(InputAction.CallbackContext callbackContext) {
        if(GameManager.Instance.gameState == GameState.PLAY) {
            PlayerState playerState = PlayerManager.Instance.playerState;
            if(playerState != PlayerState.JUMPING && playerState != PlayerState.LANDING) {
                animator.SetBool("Jump", true);
                playerController.BeginJump(gravityScale, jumpPower);
                PlayerManager.Instance.SetPlayerState(PlayerState.JUMPING);
            }
        }
    }

    void RunStart(InputAction.CallbackContext callbackContext) {
        if(GameManager.Instance.gameState == GameState.PLAY) {
            animator.SetBool("Run", true);
            currentSpeed = runSpeed;
        }
    }

    void RunStop(InputAction.CallbackContext callbackContext) {
        animator.SetBool("Run", false);
        currentSpeed = walkSpeed;
    }

    // Function that is executed everytime the player state changes
    void MovementOnPlayerStateChange(PlayerState state) {
        if(state == PlayerState.ATTACKING) {
            moveDirection = new Vector2(0, 0);
        }
    } 

    private void FinishLanding() {
        PlayerManager.Instance.SetPlayerState(PlayerState.IDLING);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            bool stoppedJump = playerController.StopJump();
            if(stoppedJump) {
                animator.SetBool("Jump", false);
                other.gameObject.SetActive(false);
            }
        }
    }
}
