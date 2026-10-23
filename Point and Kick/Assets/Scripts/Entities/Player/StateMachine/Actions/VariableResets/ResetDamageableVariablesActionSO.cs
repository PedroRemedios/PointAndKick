using Entities.HealthStatus.Damageables;
using Entities.Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ResetDamageableVariablesAction", menuName = "State Machines/Actions/Reset Damageable Variables Action")]
public class ResetDamageableVariablesActionSO : StateActionSO<ResetDamageableVariablesAction> { }

public class ResetDamageableVariablesAction : StateAction {
	private Damageable _damageable;
	
	public override void Awake(StateMachine stateMachine) {
		_damageable = stateMachine.GetComponent<Damageable>();
	}
	
	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		_damageable.isStunned = false;
		_damageable.resumingKnocked = false;
		_damageable.takingHeavyAttack = false;
		_damageable.takingLightAttack = false;
	}
}
