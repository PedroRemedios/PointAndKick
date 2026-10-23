using Entities;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ResetFinishAnimationAction", menuName = "State Machines/Actions/Reset Finish Animation Action")]
public class ResetFinishAnimationActionSO : StateActionSO<ResetFinishAnimationAction> { }

public class ResetFinishAnimationAction : StateAction {
	private Controller _controller;
	
	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
	}
	
	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		_controller.finishAnimation = false;
	}
}
