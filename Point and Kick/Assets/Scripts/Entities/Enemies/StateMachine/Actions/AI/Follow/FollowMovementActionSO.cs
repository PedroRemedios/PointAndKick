using Combat.CombatElements;
using Entities.Enemies;
using Entities.Enemies.Pathfinding;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "FollowMovementAction", menuName = "State Machines/Actions/Follow Movement Action")]
public class FollowMovementActionSO : StateActionSO<FollowMovementAction> {
	public CombatChannel combatChannel;
}

public class FollowMovementAction : StateAction {
	private EnemyController _enemyController;
	private EnemyFollowAI _enemyAI;
	private CombatChannel _combatChannel;
	
	private readonly Vector2 sideDistance = new Vector2(1.6f, 0);
	
	private new FollowMovementActionSO OriginSO => (FollowMovementActionSO) base.OriginSO;
	public override void Awake(StateMachine stateMachine) {
		_enemyController = stateMachine.GetComponent<EnemyController>();
		_enemyAI = stateMachine.GetComponent<EnemyFollowAI>();
		_combatChannel = OriginSO.combatChannel;
	}
	
	public override void OnUpdate() { 
		if (_enemyController.IsAbleToHit()) {
			_enemyController.SelectAction(_enemyAI.SelectAction(EnemyMovesetType.NormalAttack));
			_enemyAI.reachedEndOfPath = true;
		}

		if (!_combatChannel.CheckEnemyOnLock(_enemyController.gameObject)) {
			_enemyController.SelectAction(_enemyAI.SelectAction(EnemyMovesetType.Transition));
			_enemyAI.reachedEndOfPath = true;
		}
		
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
	
	public override void OnStateEnter() {
		_enemyAI.OnPathfindingPositionRequested += GeneratePathfindingPosition;
		_enemyAI.StartPathfinding();
	}
	
	public override void OnStateExit() {
		_enemyAI.StopPathfinding();
		_enemyAI.OnPathfindingPositionRequested -= GeneratePathfindingPosition;
	}

	private Vector2 GeneratePathfindingPosition() {
		if (_enemyAI.isFollowingRight)
			return (Vector2) _enemyController.playerTransform.position + sideDistance;
		return (Vector2) _enemyController.playerTransform.position - sideDistance;
	}
}
