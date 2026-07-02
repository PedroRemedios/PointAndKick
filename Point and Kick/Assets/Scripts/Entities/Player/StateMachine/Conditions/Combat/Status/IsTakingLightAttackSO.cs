using Entities.HealthStatus.Damageables;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsTakingLightAttack", menuName = "State Machines/Conditions/Is Taking Light Attack")]
public class IsTakingLightAttackSO : StateConditionSO<IsTakingLightAttack> {
}

public class IsTakingLightAttack : Condition {
	private Damageable _damageable;
	public override void Awake(StateMachine stateMachine) {
		_damageable = stateMachine.GetComponent<Damageable>();
	}
	
	protected override bool Statement() {
		return _damageable.takingLightAttack;
	}
}
