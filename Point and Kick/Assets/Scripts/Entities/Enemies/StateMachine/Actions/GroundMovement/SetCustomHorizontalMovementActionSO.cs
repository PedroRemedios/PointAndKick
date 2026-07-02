using Entities;
using Entities.HealthStatus.Damageables;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SetCustomHorizontalMovementAction", menuName = "State Machines/Actions/Set Custom Horizontal Movement Action")]
public class SetCustomHorizontalMovementActionSO : StateActionSO<SetCustomHorizontalMovementAction> {
	public float movementSpeed;
}

public class SetCustomHorizontalMovementAction : StateAction {
	private Controller _controller;
	private new SetCustomHorizontalMovementActionSO OriginSO => (SetCustomHorizontalMovementActionSO) base.OriginSO;

	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
	}
	
	public override void OnUpdate() { }

	public override void OnStateEnter() {
		_controller.movementVector.x = OriginSO.movementSpeed * _controller.characterTransform.transform.lossyScale.x;
		_controller.movementVector.y = 0f;
	}
}
