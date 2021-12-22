using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject groundGameObject;
    private Animator animator;

    [SerializeField] private float gravityScale = 5f;
    [SerializeField] private float jumpPower = 10f;
    private bool isJumping = false;
    public float jumpVelocity = 0;
    private float gravity = -9.81f;
    
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        groundGameObject = GameObject.Find("Jump Ground Check");
        groundGameObject.SetActive(false);
        
        // Finds the active player, be it Alex or Fred
        animator = GameObject.FindGameObjectWithTag("Character").GetComponent<Animator>();
    }

    void Update()
    {
        if(isJumping) {
            jumpVelocity += gravity * gravityScale * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && !isJumping) {
            jumpVelocity = jumpPower;
            isJumping = true;
            groundGameObject.SetActive(true);

            animator.SetBool("Jump", true);
        }
    }

    private void FixedUpdate() {
        groundGameObject.transform.position = playerController.getGroundPosition();
        transform.Translate(0, jumpVelocity * Time.fixedDeltaTime, 0);

        if(transform.localPosition.y < 0) {
            transform.Translate(0, -transform.localPosition.y, 0);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground") && jumpVelocity < 0) {
            animator.SetBool("Jump", false);

            jumpVelocity = 0;
            isJumping = false;
            other.gameObject.SetActive(false);
        }
    }
}
