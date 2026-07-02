using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using Entities.Player.Movement;
using UnityEngine;

public class StairsMovement : MonoBehaviour
{
    private PlayerController _playerController;
    private float minX;
    private float maxX;
    private float centerX;
    [SerializeField] private float slope;

    void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

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
            _playerController.groundSlope = slope;
            float playerX = other.gameObject.transform.position.x;

            if(gameState == GameState.EXPLORATION) {
                if(playerX < centerX) {
                    _playerController.scaleInclineBound = minX;
                }
                else {
                    _playerController.scaleInclineBound = maxX;
                }
            }
            else {
                if(playerX > centerX) {
                    _playerController.scaleInclineBound = minX;
                }
                else {
                    _playerController.scaleInclineBound = maxX;
                }                
            }

        }      
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            _playerController.groundSlope = 0;
            _playerController.scaleInclineBound = 0;
        }         
    }
}
