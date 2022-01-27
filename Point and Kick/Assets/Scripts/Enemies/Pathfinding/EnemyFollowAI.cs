using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
public class EnemyFollowAI : MonoBehaviour {
    private EnemyController enemyController;
    private Transform targetPosition;
    private Vector2 seekPosition;


    private Seeker seeker;
    public Path path;

    public float speed = 2;
    public float nextWaypointDistance = 3;
    private int currentWaypoint = 0;

    private bool reachedEndOfPath = false;
    private bool playerMoved = true;
    private bool isFacingLeft = true;

    public void Start () {
        enemyController = GetComponent<EnemyController>();
        seeker = GetComponent<Seeker>();

        targetPosition = GameObject.FindWithTag("Player").transform;
        InvokeRepeating("UpdatePath", 0, 0.5f);
    }

    private void UpdatePath() {
        if (playerMoved) {
            reachedEndOfPath = false;
            playerMoved = false;
            seeker.CancelCurrentPathRequest();

            Vector2 closestPlayerSide = GetClosestSide((Vector2) transform.position, (Vector2) targetPosition.position);
            seeker.StartPath(transform.position, closestPlayerSide, OnPathComplete);
            seekPosition = targetPosition.position;
        }
    }

    private Vector2 GetClosestSide(Vector2 enemyPosition, Vector2 playerPosition) {
        Vector2 sideDistance = new Vector2(1.75f, 0);
        float leftDistance = Vector2.Distance(enemyPosition, playerPosition - sideDistance);
        float rightDistance = Vector2.Distance(enemyPosition, playerPosition + sideDistance);

        return leftDistance < rightDistance ? playerPosition - sideDistance : playerPosition + sideDistance;
    }

    public void OnPathComplete (Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void CheckDirection() {
        if((isFacingLeft && transform.position.x < targetPosition.position.x) || (!isFacingLeft && transform.position.x > targetPosition.position.x)) {
            isFacingLeft = !isFacingLeft;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Update() {
        if(!enemyController.IsEnemyState(EnemyState.DEAD)) {
            CheckDirection();
            if (seekPosition != (Vector2) targetPosition.position) {
                playerMoved = true;
            }

            if (path == null) {
                return;
            }

            while (!reachedEndOfPath) {
                float distanceToWaypoint = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
                if (distanceToWaypoint < nextWaypointDistance) {
                    if (currentWaypoint + 1 < path.vectorPath.Count) {
                        currentWaypoint++;
                    } else {
                        // TODO: Verification using Raycast with the possible enemy attack and the Character
                        if (distanceToWaypoint < 0.1) {
                            reachedEndOfPath = true;
                        }
                        break;
                    }
                } else {
                    break;
                }
            }
            
            if (enemyController.IsEnemyState(EnemyState.NORMAL)) {
                if(!reachedEndOfPath)
                    enemyController.Move((path.vectorPath[currentWaypoint] - transform.position).normalized);
                else {
                    enemyController.Move(Vector2.zero);
                    enemyController.Attack();
                }
            }
            else
                enemyController.Move(Vector2.zero);
        }
    }
}