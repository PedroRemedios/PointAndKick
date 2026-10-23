using System;
using Entities.Enemies;
using Entities.Enemies.Pathfinding;
using GameFlow.EventChannels.SpecificEC;
using Pathfinding;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "MoveAroundMovementAction", menuName = "State Machines/Actions/Move Around Movement Action")]
public class MoveAroundMovementActionSO : StateActionSO<MoveAroundMovementAction> { }

public class MoveAroundMovementAction : StateAction {
	private EnemyController _enemyController;
	private EnemyFollowAI _enemyAI;
	private Vector2 pathfindingPosition;
	
	private readonly Vector2 sideDistance = new Vector2(1.6f, 0);
	
	private new FollowMovementActionSO OriginSO => (FollowMovementActionSO) base.OriginSO;
	public override void Awake(StateMachine stateMachine) {
		_enemyController = stateMachine.GetComponent<EnemyController>();
		_enemyAI = stateMachine.GetComponent<EnemyFollowAI>();
	}
	
	public override void OnUpdate() { 
		if (_enemyAI.path == null) {
			_enemyController.rawGroundMovement = Vector2.zero;
			return;
		}

		if (_enemyAI.reachedEndOfPath) {
			_enemyController.rawGroundMovement = Vector2.zero;
			_enemyController.SelectAction(_enemyAI.SelectAction(EnemyMovesetType.Transition));
			return;
		}

		while (!_enemyAI.reachedEndOfPath) {
			float distanceToWaypoint = Vector2.Distance(_enemyController.characterTransform.position, _enemyAI.path.vectorPath[_enemyAI.currentWaypoint]);
			if (distanceToWaypoint < _enemyAI.nextWaypointDistance) {
				if (_enemyAI.currentWaypoint + 1 < _enemyAI.path.vectorPath.Count)
					_enemyAI.currentWaypoint++;
				else
					break;
			}
			else
				break;
			
			_enemyController.rawGroundMovement = (_enemyAI.path.vectorPath[_enemyAI.currentWaypoint] - _enemyController.characterTransform.position).normalized;
		}
	}
	
	public override void OnStateEnter() {
		_enemyAI.OnPathfindingPositionRequested += GeneratePathfindingPosition;
		_enemyAI.StartPathfinding();
		pathfindingPosition = Vector2.zero;
	}
	
	public override void OnStateExit() {
		_enemyAI.StopPathfinding();
		_enemyAI.OnPathfindingPositionRequested -= GeneratePathfindingPosition;
	}

	private Vector2 GeneratePathfindingPosition() {
		if (pathfindingPosition == Vector2.zero) {
			float playerProximityAngle = Random.Range(-Mathf.PI, Mathf.PI);
			Debug.Log(playerProximityAngle);
			float playerProximity = Random.Range(1.5f, 2.5f);
			
			pathfindingPosition = (Vector2) _enemyController.playerTransform.position
			                              + new Vector2(Mathf.Cos(playerProximityAngle) * playerProximity,
				                              Mathf.Sin(playerProximityAngle) * playerProximity);
			
			Debug.Log(pathfindingPosition);
		}
		
		Vector2 enemyPosition = _enemyController.characterTransform.position;
		float distance = Vector2.Distance(enemyPosition, pathfindingPosition);
		
		return distance < 0.5f ? Vector2.zero : pathfindingPosition;
	}
}
