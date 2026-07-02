using Entities;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ApplyGravityAction", menuName = "State Machines/Actions/Apply Gravity Action")]
public class ApplyGravityActionSO : StateActionSO<ApplyGravityAction> { }

public class ApplyGravityAction : StateAction {
	private Controller _controller;
	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
	}
	
	public override void OnUpdate() {
		_controller.aerialMovement += -9.81f * _controller.characterWeight * Time.deltaTime;
	}
}
