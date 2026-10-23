using Entities;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SlowGroundMovementAction", menuName = "State Machines/Actions/Slow Ground Movement Action")]
public class SlowGroundMovementActionSO : StateActionSO<SlowGroundMovementAction> {
	public float slowdownFactor;
}

public class SlowGroundMovementAction : StateAction {
	private Controller _controller;
	private new SlowGroundMovementActionSO OriginSO => (SlowGroundMovementActionSO) base.OriginSO;

	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
	}

	public override void OnUpdate() {
		if (_controller.movementVector.x < 0)
			_controller.movementVector.x += OriginSO.slowdownFactor;
		if (_controller.movementVector.x > 0)
			_controller.movementVector.x -= OriginSO.slowdownFactor;
		if (_controller.movementVector.y < 0)
			_controller.movementVector.y += OriginSO.slowdownFactor;
		if (_controller.movementVector.y > 0)
			_controller.movementVector.y -= OriginSO.slowdownFactor;
	}
}
