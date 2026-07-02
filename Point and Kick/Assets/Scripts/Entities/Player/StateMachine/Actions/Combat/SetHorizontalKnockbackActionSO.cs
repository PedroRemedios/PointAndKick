using Entities;
using Entities.HealthStatus.Damageables;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SetHorizontalKnockbackAction", menuName = "State Machines/Actions/Set Horizontal Knockback Action")]
public class SetHorizontalKnockbackActionSO : StateActionSO<SetHorizontalKnockbackAction> { }

public class SetHorizontalKnockbackAction : StateAction {
	private Controller _controller;
	private Damageable _damageable;

	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
		_damageable = stateMachine.GetComponent<Damageable>();
	}
	
	public override void OnUpdate() { }

	public override void OnStateEnter() {
		_controller.movementVector.x = _damageable.currentAttackerTransform.position.x >= _controller.characterTransform.position.x ? 
									   -_damageable.currentlyTakingAttack.knockbackForce.x : _damageable.currentlyTakingAttack.knockbackForce.x;
		_controller.movementVector.y = 0f;
	}
}
