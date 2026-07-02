using Entities;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ActivateHorizontalInputMovement", menuName = "State Machines/Actions/Activate Horizontal Input Movement")]
public class ActivateHorizontalInputMovementSO : StateActionSO<ActivateHorizontalInputMovement> { }

public class ActivateHorizontalInputMovement : StateAction {
	private Controller _controller;

	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
	}
	
	public override void OnUpdate() {
		_controller.movementVector.x = _controller.adjustedGroundMovement.x * _controller.movementSpeed;
		_controller.movementVector.y = 0;
	}
}
