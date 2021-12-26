using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private EdgeCollider2D sceneBorderCollider;
    private CapsuleCollider2D playerCollider;
    private Rigidbody2D playerRigidBody;
    private GameObject groundGameObject;
    public GameObject[] npcList;

    // Scaling Variables
    [SerializeField] private float scalePerUnit = 0.05f;
    private Vector2 scaleReference = new Vector2(1, 1);
    private float colliderDistance;

    // Slope Variables
    private float slope = 0;
    public float groundSlope {
        get { return slope; }
        set { slope = value; }
    }
    
    // Jump Variables
    private float groundCheckError = 0.015f;
    private float gravity = -9.81f;
    private float jumpVelocity = 0;

    // Control Variables
    private bool isFacingRight = true;
    private bool isJumping = false;

    void Start()
    {
        sceneBorderCollider = GameObject.FindWithTag("Scale Border").GetComponent<EdgeCollider2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerRigidBody = GetComponent<Rigidbody2D>();

        groundGameObject = GameObject.FindWithTag("Ground");
        groundGameObject.SetActive(false);
    }

    void Update()
    {
        ScalePlayer();
        MoveGroundCheck();
    }

    private void ScalePlayer() {
        colliderDistance = playerCollider.Distance(sceneBorderCollider).distance;
        transform.localScale = scaleReference * (1 + scalePerUnit * colliderDistance);
    }

    private void MoveGroundCheck() {
        float groundY = playerRigidBody.position.y + (playerCollider.offset.y - playerCollider.size.y / 2) * transform.localScale.y; // - groundCheckError;
        groundGameObject.transform.position = new Vector2(0, groundY);
    }

    public void Move(Vector2 moveDirection, float currentSpeed) {
        Vector2 position = playerRigidBody.position;
        position.x = position.x + currentSpeed * moveDirection.x * Time.fixedDeltaTime;
        position.y = position.y + currentSpeed * moveDirection.y * Time.fixedDeltaTime
                   + currentSpeed * moveDirection.x * Time.fixedDeltaTime * slope;

        playerRigidBody.MovePosition(position);
        UpdateEntityOrder();
    }

    public void Jump(Transform characterTransform) {
        characterTransform.Translate(0, jumpVelocity * Time.fixedDeltaTime, 0);
        if(characterTransform.localPosition.y < 0) {
            characterTransform.Translate(0, -characterTransform.localPosition.y, 0);
        }
    }

    public bool BeginJump(float gravityScale, float jumpPower) {
        if(isJumping) {
            jumpVelocity += gravity * gravityScale * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && !isJumping) {
            jumpVelocity = jumpPower;
            isJumping = true;
            
            InitEntityOrdering();
            groundGameObject.SetActive(true);
            return true;
        }

        return false;
    }

    public bool StopJump() {
        if(jumpVelocity < 0) {
            jumpVelocity = 0;
            isJumping = false;

            EndEntityOrdering();
            return true;
        }

        return false;
    }

    public void CheckDirection(float horizontalInput) {
        if((horizontalInput < 0 && isFacingRight) || (horizontalInput > 0 && !isFacingRight)) {
            isFacingRight = !isFacingRight;
            scaleReference.x *= -1;
            transform.localScale = scaleReference;
        }
    }

    /* 
        SPRITE SORTING WHILE JUMPING - WORKAROUND
        - Problem: Character sprite pivot point goes up while jumping, messing the sorting order of the sprites.
        . Solution: Use the Player (ground) Y-coordinate and update all NPC sorting order, depending if they are above or below the Player.
    */

    private void InitEntityOrdering() {
        npcList = GameObject.FindGameObjectsWithTag("NPC");
        UpdateEntityOrder();
    }

    private void UpdateEntityOrder() {
        float playerY = playerRigidBody.position.y;
        foreach(GameObject npc in npcList) {
            SpriteRenderer npcSprite = npc.GetComponent<SpriteRenderer>();

            float npcY = npc.transform.position.y;
            if(npcY < playerY) {
                npcSprite.sortingOrder = 1;
            }
            else if(npcY > playerY) {
                npcSprite.sortingOrder = -1;
            }
        }
    }

    private void EndEntityOrdering() {
        ResetEntityOrder();
        npcList = new GameObject[0];
    }

    private void ResetEntityOrder() {
        foreach(GameObject npc in npcList) {
            SpriteRenderer npcSprite = npc.GetComponent<SpriteRenderer>();
            npcSprite.sortingOrder = 0;
        }
    }    
}
