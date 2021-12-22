using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsMovement : MonoBehaviour
{
    [SerializeField] private float slope;
    private float previousSlope;
    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
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
