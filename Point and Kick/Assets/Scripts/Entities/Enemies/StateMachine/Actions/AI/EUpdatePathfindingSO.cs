using Entities.Enemies;
using Entities.Enemies.Pathfinding;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "EUpdatePathfinding", menuName = "State Machines/Actions/EUpdate Pathfinding")]
public class EUpdatePathfindingSO : StateActionSO<EUpdatePathfinding> { }

public class EUpdatePathfinding : StateAction {
	private EnemyController _enemyController;
	private EnemyAI _enemyAI;
	public override void Awake(StateMachine stateMachine) {
		_enemyController = stateMachine.GetComponent<EnemyController>();
		_enemyAI = stateMachine.GetComponent<EnemyAI>();
	}
	
	public override void OnUpdate() {
		if(_enemyController.IsAbleToHit()) {
			_enemyController.SelectAction(_enemyAI.SelectAction(EnemyMovesetType.NormalAttack));
			_enemyAI.reachedEndOfPath = true;
		}
		
		if (_enemyAI.reachedEndOfPath || !_enemyAI.PlayerInVision())
			_enemyController.rawGroundMovement = Vector2.zero;
		
		if (_enemyAI.path == null)
			return;

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
}
