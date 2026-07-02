using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    private SpriteRenderer foregroundSprite;
    private Camera mainCamera;

    public float parallaxFactor;
    public bool leftSide;
    public bool rightSide;
    private float uncenteredXFactor;

    private void Awake() {
        foregroundSprite = GetComponent<SpriteRenderer>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
    private void Start() {
        transform.localScale = new Vector2(1, 1);

        float halfCameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float cameraMovableX = 2f * (foregroundSprite.bounds.extents.x - halfCameraWidth);

        uncenteredXFactor = 0;
        if (leftSide) {
            cameraMovableX += halfCameraWidth;
            uncenteredXFactor += halfCameraWidth / 2f;
        }
        if (rightSide) {
            cameraMovableX += halfCameraWidth;
            uncenteredXFactor -= halfCameraWidth / 2f;
        }

        float scaleFactor = Mathf.Abs(parallaxFactor * cameraMovableX);
        float xScale = (2f * foregroundSprite.bounds.extents.x + scaleFactor) / (2f * foregroundSprite.bounds.extents.x);
        
        transform.localScale = new Vector2(xScale, 1);
    }
    public void Move(float cameraPos, float xDifference) {
        Vector3 newPos = transform.localPosition;
        newPos.x = (cameraPos - (xDifference + uncenteredXFactor)) * parallaxFactor;
        transform.localPosition = newPos;
    }
}
