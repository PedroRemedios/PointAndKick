using Entities;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ApplyMovementVector", menuName = "State Machines/Actions/Apply Movement Vector")]
public class ApplyMovementVectorSO : StateActionSO<ApplyMovementVector> { }

public class ApplyMovementVector : StateAction {
	private Controller _controller;
	private Rigidbody2D _rigidBody;
	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
		_rigidBody = stateMachine.GetComponent<Rigidbody2D>();
	}
	
	public override void OnUpdate() {
		Vector2 position = _rigidBody.position;
		position.x += _controller.movementVector.x * Time.fixedDeltaTime;
		position.y += _controller.movementVector.y * Time.fixedDeltaTime;

		_rigidBody.MovePosition(position);
	}
}
