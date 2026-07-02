using Entities;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ActivateInputMovement", menuName = "State Machines/Actions/Activate Input Movement")]
public class ActivateInputMovementSO : StateActionSO<ActivateInputMovement> { }

public class ActivateInputMovement : StateAction {
	private Controller _controller;

	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
	}
	
	public override void OnUpdate() {
		_controller.movementVector.x = _controller.adjustedGroundMovement.x * _controller.movementSpeed;
		_controller.movementVector.y = _controller.adjustedGroundMovement.y * _controller.movementSpeed;
	}
}
