using Entities.HealthStatus.Damageables;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsTakingHeavyAttack", menuName = "State Machines/Conditions/Is Taking Heavy Attack")]
public class IsTakingHeavyAttackSO : StateConditionSO<IsTakingHeavyAttack> {
}

public class IsTakingHeavyAttack : Condition {
	private Damageable _damageable;
	public override void Awake(StateMachine stateMachine) {
		_damageable = stateMachine.GetComponent<Damageable>();
	}
	
	protected override bool Statement() {
		return _damageable.takingHeavyAttack;
	}
}
