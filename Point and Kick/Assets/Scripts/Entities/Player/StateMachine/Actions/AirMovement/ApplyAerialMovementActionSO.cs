using Entities;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ApplyAerialMovementAction", menuName = "State Machines/Actions/Apply Aerial Movement Action")]
public class ApplyAerialMovementActionSO : StateActionSO<ApplyAerialMovementAction> { }

public class ApplyAerialMovementAction : StateAction {
	private Controller _controller;
	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
	}
	
	public override void OnUpdate() {
		_controller.spriteTransform.Translate(0, _controller.aerialMovement * Time.deltaTime, 0);
		if(_controller.spriteTransform.localPosition.y < 0)
			_controller.spriteTransform.localPosition = Vector3.zero;
	}
}
