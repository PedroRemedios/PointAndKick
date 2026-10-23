using Entities;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "StopGroundMovementAction", menuName = "State Machines/Actions/Stop Ground Movement Action")]
public class StopGroundMovementActionSO : StateActionSO<StopGroundMovementAction> { }

public class StopGroundMovementAction : StateAction {
	private Controller _controller;

	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
	}

	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		_controller.movementVector = Vector2.zero;
	}
}
