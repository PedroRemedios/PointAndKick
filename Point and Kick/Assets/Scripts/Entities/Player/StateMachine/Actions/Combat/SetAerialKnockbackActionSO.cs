using Entities;
using Entities.HealthStatus.Damageables;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SetAerialKnockbackAction", menuName = "State Machines/Actions/Set Aerial Knockback Action")]
public class SetAerialKnockbackActionSO : StateActionSO<SetAerialKnockbackAction> { }

public class SetAerialKnockbackAction : StateAction {
	private Controller _controller;
	private Damageable _damageable;

	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
		_damageable = stateMachine.GetComponent<Damageable>();
	}
	
	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		_controller.aerialMovement = _damageable.currentlyTakingAttack.knockbackForce.y;
	}
}
