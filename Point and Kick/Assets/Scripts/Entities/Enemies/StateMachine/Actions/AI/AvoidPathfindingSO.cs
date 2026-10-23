using Entities.Enemies;
using Entities.Enemies.Pathfinding;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AvoidPathfinding", menuName = "State Machines/Actions/Avoid Pathfinding")]
public class AvoidPathfindingSO : StateActionSO<AvoidPathfinding> {
	public CombatChannel combatChannel;
}

public class AvoidPathfinding : StateAction {
	private EnemyController _enemyController;
	private EnemyAvoidAI _enemyAI;
	private CombatChannel _combatChannel;
	
	private new AvoidPathfindingSO OriginSO => (AvoidPathfindingSO) base.OriginSO;
	public override void Awake(StateMachine stateMachine) {
		_enemyController = stateMachine.GetComponent<EnemyController>();
		_enemyAI = stateMachine.GetComponent<EnemyAvoidAI>();
		_combatChannel = OriginSO.combatChannel;
	}
	
	public override void OnUpdate() {
		bool isPlayerVisible = _enemyAI.PlayerInVision();
		_enemyAI.SetClosestEnemy(null);

		if (_enemyAI.reachedEndOfPath || !isPlayerVisible)
			_enemyController.rawGroundMovement = Vector2.zero;
		
		if (!_combatChannel.IsEnemyCount(2)) {
			if (Time.time - _enemyAI.timeOfLastMove >= _enemyAI.cooldown) {
				_enemyAI.timeOfLastMove = Time.time;
				_enemyController.SelectAction(_enemyAI.SelectAction(EnemyMovesetType.PanicAttack));
			}
		}
		else if (isPlayerVisible && IsEnemyFurther()) {
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
		else if (_enemyController.IsAbleToHit()) {
			_enemyController.SelectAction(_enemyAI.SelectAction(EnemyMovesetType.DefensiveAttack));
		}
		else {
			if (Time.time - _enemyAI.timeOfLastMove >= _enemyAI.cooldown) {
				_enemyAI.timeOfLastMove = Time.time;
				_enemyController.SelectAction(_enemyAI.SelectAction(EnemyMovesetType.NormalAttack));
			}
		}
	}

	private bool IsEnemyFurther() {
		Transform closestVisibleEnemy = _combatChannel.GetClosestVisibleEnemy(_enemyController.characterTransform, _enemyAI.visionArea);

		if (closestVisibleEnemy == null)
			return false;
		
		float selfDistanceToPlayer = Vector2.Distance(_enemyController.characterTransform.position,
			_enemyController.playerTransform.position);
		float otherDistanceToPlayer = Vector2.Distance(closestVisibleEnemy.position,
			_enemyController.playerTransform.position);
		
		_enemyAI.SetClosestEnemy(closestVisibleEnemy);

		if ((otherDistanceToPlayer + 2) >= selfDistanceToPlayer) return true;
		
		_enemyAI.reachedEndOfPath = true;
		return false;

	}
}
