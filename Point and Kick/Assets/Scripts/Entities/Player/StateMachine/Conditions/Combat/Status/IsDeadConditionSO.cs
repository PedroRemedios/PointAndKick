using Entities.HealthStatus.Damageables;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsDeadCondition", menuName = "State Machines/Conditions/Is Dead Condition")]
public class IsDeadConditionSO : StateConditionSO<IsDeadCondition> {
}

public class IsDeadCondition : Condition {
	private Damageable _damageable;
	public override void Awake(StateMachine stateMachine) {
		_damageable = stateMachine.GetComponent<Damageable>();
	}
	
	protected override bool Statement() {
		return _damageable.isDead;
	}
}
