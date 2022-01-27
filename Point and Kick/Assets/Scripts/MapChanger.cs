using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapChanger : MonoBehaviour
{
    /*
        The goal of this script is to apply all information when transitioning from one map to another IN THE SAME SCENE.
        Information needed: New position, New camera confiner, transition itself
        Helper Things: Pass the goal MapChanger (Each object associated with this script only has 1 transition, so there can be multiple of them per map)
    */
    public GameObject destinationMap;
    public GameObject destinationTransition;
    public Animator transitionAnimator;
    [SerializeField] private float transitionTime = 1f;

    private CinemachineConfiner cameraConfinerComponent;
    private GameObject player;
    private float minX;
    private float maxX;
    private float centerX;

    void Start()
    {
        cameraConfinerComponent = GameObject.FindWithTag("CM Camera").gameObject.transform.Find("CM Normal Camera").GetComponent<CinemachineConfiner>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            GameState gameState = GameManager.Instance.gameState;
            if(gameState == GameState.PLAY) {
                GameManager.Instance.SetGameState(GameState.TRANSITION_BEGIN);
                StartCoroutine(ChangeMap(other.transform.localScale.x));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        GameState gameState = GameManager.Instance.gameState;
        if(gameState == GameState.TRANSITION_END) {
            GameManager.Instance.SetGameState(GameState.PLAY);
        }
    }
    
    /// ChangeMap | (float scaleX) -> ()
    /// Summary: After the transition begins, wait for half the transition time, start the transition animation depending on the player's orientation,
    ///          wait for another half and recalculate player and camera positions. After the calculations are done, end the transition animation.
    /// Note: It's important that the second yield == transition animation time. Cannot enforce on code.
    IEnumerator ChangeMap(float scaleX) {
        // yield return new WaitForSeconds(transitionTime / 2);

        if(scaleX < 0) {
            transitionAnimator.SetBool("Transition Right", true);
        }
        else {
            transitionAnimator.SetBool("Transition Left", true);
        }

        yield return new WaitForSeconds(transitionTime);

        player.transform.position = destinationTransition.transform.position;
        PolygonCollider2D newCameraConfinerObject = destinationMap.transform.Find("Camera Confiner").GetComponent<PolygonCollider2D>();
        cameraConfinerComponent.m_BoundingShape2D = newCameraConfinerObject;

        // Little hack to make sure the game state change to transition end is not called before the one from transition end to play.
        yield return new WaitForSeconds(transitionTime / 10);
        GameManager.Instance.SetGameState(GameState.TRANSITION_END);

        if(scaleX < 0) {
            transitionAnimator.SetBool("Transition Right", false);
        }
        else {
            transitionAnimator.SetBool("Transition Left", false);
        }
    }
}
