using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScaling : MonoBehaviour
{
    [SerializeField] private float scalePerUnit = 0.05f;
    public float colliderDistance;
    private Vector2 scaleReference;

    private EdgeCollider2D sceneBorderCollider;
    private CapsuleCollider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        sceneBorderCollider = GameObject.Find("Scaling Border").GetComponent<EdgeCollider2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();

        scaleReference = new Vector2(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        colliderDistance = playerCollider.Distance(sceneBorderCollider).distance;

        transform.localScale = scaleReference * (1 + scalePerUnit * colliderDistance);
    }

    public void flipX() {
        scaleReference.x *= -1;
        transform.localScale = scaleReference;
    }
}
