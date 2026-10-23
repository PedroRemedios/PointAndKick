using Entities.Enemies;
using Entities.Enemies.Pathfinding;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "BackUpMovementAction", menuName = "State Machines/Actions/Back Up Movement Action")]
public class BackUpMovementActionSO : StateActionSO<BackUpMovementAction> { }

public class BackUpMovementAction : StateAction {
	private EnemyController _enemyController;
	private EnemyFollowAI _enemyAI;
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
	}
	
	public override void OnStateExit() {
		_enemyAI.StopPathfinding();
		_enemyAI.OnPathfindingPositionRequested -= GeneratePathfindingPosition;
	}

	private Vector2 GeneratePathfindingPosition() {
		Vector2 enemyPosition = _enemyController.characterTransform.position;
		Vector2 playerPosition = _enemyController.playerTransform.position;
            
		float distance = Vector2.Distance(enemyPosition, playerPosition);
		if (distance > 5f)
			return Vector2.zero;
		
		float distanceFactor = 5f / distance;
		
		Vector2 finalPosition = enemyPosition + new Vector2((enemyPosition.x - playerPosition.x) * distanceFactor,
			(enemyPosition.y - playerPosition.y) * distanceFactor);
		return finalPosition;
	}
}
