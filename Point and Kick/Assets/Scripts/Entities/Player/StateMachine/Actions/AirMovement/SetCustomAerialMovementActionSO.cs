using Entities;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SetCustomAerialMovementAction",
	menuName = "State Machines/Actions/Set Custom Aerial Movement Action")]
public class SetCustomAerialMovementActionSO : StateActionSO<SetCustomAerialMovementAction> {
	public float jumpingPower;
}

public class SetCustomAerialMovementAction : StateAction {
	private Controller _controller;
	private new SetCustomAerialMovementActionSO OriginSO => (SetCustomAerialMovementActionSO) base.OriginSO;

	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
	}
	
	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		_controller.aerialMovement = OriginSO.jumpingPower;
	}
}
