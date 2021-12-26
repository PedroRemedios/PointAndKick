using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;

    private float horizontalInput;
    private float verticalInput;
    private Vector2 moveDirection;

    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float runSpeed = 9f;
    private float currentSpeed = 4f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float gravityScale = 5f;

    void Start() {
        Application.targetFrameRate = 200;

        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
        playerController.CheckDirection(horizontalInput);

        // Movement Animation | Idle < Speed = 0.1 < Walk/Run
        animator.SetFloat("Speed", moveDirection.magnitude);

        // Running Animation | No Shift = Idle/Walk; Shift = Run
        // TODO: Change Controls | Shift Toggle => Double Tap Direction
        bool isRunning = Input.GetButton("Run");
        animator.SetBool("Run", isRunning);
        currentSpeed = isRunning ? runSpeed : walkSpeed;

        CheckJump();
    }

    void FixedUpdate() {
        playerController.Move(moveDirection, currentSpeed);
        playerController.Jump(transform);
    }

    private void CheckJump() {
        bool startedJump = playerController.BeginJump(gravityScale, jumpPower);
        if(startedJump) {
            animator.SetBool("Jump", true);
        }
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
