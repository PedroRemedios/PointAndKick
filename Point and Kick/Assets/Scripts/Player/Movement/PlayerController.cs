using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CapsuleCollider2D playerCollider;
    private Rigidbody2D playerRigidBody;
    private GameObject[] npcList;

    // Scaling Variables
    [SerializeField] private float scalePerUnit = 0.05f;
    private float scaleDirection = 1;
    private float scaleBoundX = 0;
    public float scaleInclineBound {
        get { return scaleBoundX; }
        set { scaleBoundX = value; }
    }

    // Slope Variables
    private float slope = 0;
    public float groundSlope {
        get { return slope; }
        set { slope = value; }
    }
    
    // Jump Variables
    private float gravity = -9.81f;
    private float jumpVelocity = 0;
    private float gravityScale = 0;

    // Control Variables
    private bool isFacingRight = true;

    void Awake() {
        npcList = new GameObject[0];

        playerCollider = GetComponent<CapsuleCollider2D>();
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ScalePlayer();
        CalculateJumpVelocity();
    }

    // TODO: Change -3 to a variable (Where the variable is the y level where the scaling is 1)
    // TODO: Change default scale value from 1 to a variable as well (TS2 needs a bigger sprite)
    private void ScalePlayer() {
        float scaleReferenceY = -3 + (transform.position.x - scaleBoundX) * slope;
        float newScaleValue = 1 - (transform.position.y - scaleReferenceY) * scalePerUnit;

        transform.localScale = new Vector2(scaleDirection * newScaleValue, newScaleValue);
    }

    private void CalculateJumpVelocity() {
        if(PlayerManager.Instance.playerState == PlayerState.JUMPING) {
            jumpVelocity += gravity * gravityScale * Time.deltaTime;
        }
    }


    public void Move(Vector2 moveDirection, float currentSpeed) {
        Vector2 slopedMoveDirection = new Vector2(moveDirection.x, moveDirection.y + moveDirection.x * slope).normalized;
        Vector2 position = playerRigidBody.position;
        position.x = position.x + currentSpeed * slopedMoveDirection.x * Time.fixedDeltaTime;
        position.y = position.y + currentSpeed * slopedMoveDirection.y * Time.fixedDeltaTime;

        playerRigidBody.MovePosition(position);
        UpdateEntityOrder();
    }

    public void Jump(Transform characterTransform) {
        characterTransform.Translate(0, jumpVelocity * Time.fixedDeltaTime, 0);
        if(characterTransform.localPosition.y < 0) {
            characterTransform.Translate(0, -characterTransform.localPosition.y, 0);
        }
    }
    

    public void BeginJump(float characterWeight, float jumpPower) {
        jumpVelocity = jumpPower;
        gravityScale = characterWeight;
            
        InitEntityOrdering();
    }

    public bool StopJump() {
        if(jumpVelocity < 0) {
            jumpVelocity = 0;
            PlayerManager.Instance.SetPlayerState(PlayerState.LANDING);

            EndEntityOrdering();
            return true;
        }

        return false;
    }

    public void CheckDirection(float horizontalInput) {
        if((horizontalInput < 0 && isFacingRight) || (horizontalInput > 0 && !isFacingRight)) {
            isFacingRight = !isFacingRight;
            scaleDirection *= -1;
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
