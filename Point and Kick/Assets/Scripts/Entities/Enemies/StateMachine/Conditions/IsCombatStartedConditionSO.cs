using Entities.Enemies;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsCombatStartedCondition", menuName = "State Machines/Conditions/Is Combat Started Condition")]
public class IsCombatStartedConditionSO : StateConditionSO<IsCombatStartedCondition> { }

public class IsCombatStartedCondition : Condition {
	private EnemyController _controller;
	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<EnemyController>();
	}
	
	protected override bool Statement() {
		return _controller.startedCombat;
	}
}
