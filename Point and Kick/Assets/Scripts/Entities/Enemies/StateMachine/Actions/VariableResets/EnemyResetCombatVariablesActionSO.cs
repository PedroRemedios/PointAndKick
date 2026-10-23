using Entities.Enemies;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "EnemyResetCombatVariablesAction", menuName = "State Machines/Actions/Enemy Reset Combat Variables Action")]
public class EnemyResetCombatVariablesActionSO : StateActionSO<EnemyResetCombatVariablesAction> { }

public class EnemyResetCombatVariablesAction : StateAction {
	private EnemyController _enemyController;
	
	public override void Awake(StateMachine stateMachine) {
		_enemyController = stateMachine.GetComponent<EnemyController>();
	}
	
	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		_enemyController.moveSelected = null;
	}
}
