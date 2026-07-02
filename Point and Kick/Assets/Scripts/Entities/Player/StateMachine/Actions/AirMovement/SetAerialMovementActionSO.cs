using Entities;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SetAerialMovementAction", menuName = "State Machines/Actions/Set Aerial Movement Action")]
public class SetAerialMovementActionSO : StateActionSO<SetAerialMovementAction> { }

public class SetAerialMovementAction : StateAction {
	private Controller _controller;

	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
	}
	
	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		_controller.aerialMovement = _controller.data.jumpPower;
	}
}
