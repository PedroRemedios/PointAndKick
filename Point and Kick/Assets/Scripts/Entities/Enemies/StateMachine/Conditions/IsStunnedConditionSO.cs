using Entities.Enemies;
using Entities.HealthStatus.Damageables;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsStunnedCondition", menuName = "State Machines/Conditions/Is Stunned Condition")]
public class IsStunnedConditionSO : StateConditionSO<IsStunnedCondition> { }

public class IsStunnedCondition : Condition {
	private Damageable _damageable;

	public override void Awake(StateMachine stateMachine) {
		_damageable = stateMachine.GetComponent<Damageable>();
	}
	
	protected override bool Statement() {
		return _damageable.isStunned;
	}
}
