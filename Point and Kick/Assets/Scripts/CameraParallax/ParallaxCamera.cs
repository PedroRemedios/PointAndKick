using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCamera : MonoBehaviour 
{
    public delegate void ParallaxCameraDelegate(float deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;
    private float oldPosition;
    void Start() {
        oldPosition = transform.position.x;
    }
    void Update() {
        //if (transform.position.x != oldPosition) {
        if (onCameraTranslate != null) {
            float cameraPos = transform.position.x;
            onCameraTranslate(cameraPos);
        }
        //    oldPosition = transform.position.x;
        //}
    }
}