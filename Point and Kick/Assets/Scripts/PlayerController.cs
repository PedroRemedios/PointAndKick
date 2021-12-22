using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private CapsuleCollider2D capsuleCollider;
    private Animator animator;


    private float horizontalInput;
    private float verticalInput;
    private Vector2 moveDirection;

    [SerializeField] private float groundCheckError = 0.015f;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 8f;
    private float currentSpeed = 3f;
    public float groundSlope;

    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 200;
        groundSlope = 0;

        playerRigidBody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        // Finds the active player, be it Alex or Fred
        animator = GameObject.FindGameObjectWithTag("Character").GetComponent<Animator>();
    }

    void Update()
    {   
        /* MOVEMENT CALCULATIONS: IDLE, WALK AND RUN */

        // Get Movement Input Data (WASD | Arrow Keys Input)
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // If player is moving left but facing right, make it face left
        if(horizontalInput < 0 && isFacingRight) {
            FlipX();
        }
        // If player is moving right but facing left, make it face right
        else if(horizontalInput > 0 && !isFacingRight) {
            FlipX();
        }

        // Speed is the size of the movement vector in a 2D space. If lower than 0.1, we consider the player as idle
        animator.SetFloat("Speed", moveDirection.magnitude);

        // Running Toggable Animation (While Pressing Shift)
        bool isRunning = Input.GetButton("Run");
        animator.SetBool("Run", isRunning);
        currentSpeed = isRunning ? runSpeed : walkSpeed;
    }

    // Update method for all the Physics. Executed in regular intervals (0.02s, or 50fps)
    void FixedUpdate()
    {
        Vector2 position = playerRigidBody.position;
        position.x = position.x + currentSpeed * moveDirection.x * Time.fixedDeltaTime;
        position.y = position.y + currentSpeed * moveDirection.y * Time.fixedDeltaTime
                   + currentSpeed * moveDirection.x * Time.fixedDeltaTime * groundSlope;

        playerRigidBody.MovePosition(position);
    }

    // Flips the player object (sprites, colliders, etc) according to its movement on the X axis
    private void FlipX() {
        isFacingRight = !isFacingRight;

        gameObject.GetComponent<PlayerScaling>().flipX();
    }

    public Vector2 getGroundPosition() {
        float groundY = playerRigidBody.position.y + (capsuleCollider.offset.y - capsuleCollider.size.y / 2) * transform.localScale.y - groundCheckError;
        return new Vector2(0, groundY);
    }

}
