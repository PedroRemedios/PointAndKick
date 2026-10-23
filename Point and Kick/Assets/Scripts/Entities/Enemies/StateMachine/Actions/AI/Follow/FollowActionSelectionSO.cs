using System;
using Combat.CombatElements;
using Entities.Enemies;
using Entities.Enemies.Pathfinding;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "FollowActionSelection", menuName = "State Machines/Actions/Follow Action Selection")]
public class FollowActionSelectionSO : StateActionSO<FollowActionSelection> {
	public CombatChannel combatChannel;
}

public class FollowActionSelection : StateAction {	
	private EnemyController _enemyController;
	private EnemyFollowAI _enemyAI;
	private CombatChannel _combatChannel;
	
	private new FollowActionSelectionSO OriginSO => (FollowActionSelectionSO) base.OriginSO;
	public override void Awake(StateMachine stateMachine) {
		_enemyController = stateMachine.GetComponent<EnemyController>();
		_enemyAI = stateMachine.GetComponent<EnemyFollowAI>();
		_combatChannel = OriginSO.combatChannel;
	}
	
	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		float enemyActionValue = Random.Range(0f, 1f);
		
		// Enemy on Lock Point or just close to the Player
		if (_enemyController.IsAbleToHit()) {
			if (enemyActionValue < 0.5) {
				_enemyController.SelectAction(_enemyAI.SelectAction(EnemyMovesetType.NormalAttack));
				_enemyAI.reachedEndOfPath = true;
			}
			else 
				_enemyController.SelectAction(_enemyAI.SelectAction(EnemyMovesetType.BackUpMove));
		}
		else {
			PlayerLockSide lockSide = _combatChannel.LockPlayerSide(_enemyController.gameObject, false);
			if (lockSide == PlayerLockSide.None) {
				// Enemy doesnt get Lock Point and roams around
				_enemyController.SelectAction(_enemyAI.SelectAction(EnemyMovesetType.CircleMove));
			}
			else {
				// Enemy gets Lock Point and follows Player to that Point
				_enemyController.SelectAction(_enemyAI.SelectAction(EnemyMovesetType.FollowMove));
				if (lockSide == PlayerLockSide.Left)
					_enemyAI.isFollowingRight = false;
				else if (lockSide == PlayerLockSide.Right)
					_enemyAI.isFollowingRight = true;
			}
		}
	}
}
