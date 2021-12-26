using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsMovement : MonoBehaviour
{
    private PlayerController playerController;
    private float previousSlope;
    private float slope;

    void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            previousSlope = playerController.groundSlope;
            playerController.groundSlope = slope;
        }      
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            playerController.groundSlope = previousSlope;   
        }         
    }
}
