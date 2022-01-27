using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsMovement : MonoBehaviour
{
    private PlayerController playerController;
    private float minX;
    private float maxX;
    private float centerX;
    [SerializeField] private float slope;

    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        GetColliderBounds();
    }

    private void GetColliderBounds() {
        BoxCollider2D stairsTrigger = GetComponent<BoxCollider2D>();
        float center = stairsTrigger.bounds.center.x;
        float extent = stairsTrigger.bounds.extents.x;

        // Placeholder. Correct if all stairs start with a parallel step (not the case on train station 2)
        minX = center - extent;
        maxX = center + extent;
        centerX = center;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            GameState gameState = GameManager.Instance.gameState;
            playerController.groundSlope = slope;
            float playerX = other.gameObject.transform.position.x;

            if(gameState == GameState.EXPLORATION) {
                if(playerX < centerX) {
                    playerController.scaleInclineBound = minX;
                }
                else {
                    playerController.scaleInclineBound = maxX;
                }
            }
            else {
                if(playerX > centerX) {
                    playerController.scaleInclineBound = minX;
                }
                else {
                    playerController.scaleInclineBound = maxX;
                }                
            }

        }      
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            playerController.groundSlope = 0;
            playerController.scaleInclineBound = 0;
        }         
    }
}
