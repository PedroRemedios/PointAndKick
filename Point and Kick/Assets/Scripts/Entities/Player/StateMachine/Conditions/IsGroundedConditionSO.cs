using Entities;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsGroundedCondition", menuName = "State Machines/Conditions/Is Grounded Condition")]
public class IsGroundedConditionSO : StateConditionSO<IsGroundedCondition> { }

public class IsGroundedCondition : Condition {
	private Controller _controller;
	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
	}
	
	protected override bool Statement() {
		if (_controller.aerialMovement < 0)
			return _controller.spriteTransform.localPosition.y <= 0;
		return false;
	}
}
