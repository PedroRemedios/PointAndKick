using Entities;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsMovingCondition", menuName = "State Machines/Conditions/Is Moving Condition")]
public class IsMovingConditionSO : StateConditionSO<IsMovingCondition> {
	public float threshold = 0.02f;
}

public class IsMovingCondition : Condition {
	private Controller _controller;
	private new IsMovingConditionSO OriginSO => (IsMovingConditionSO) base.OriginSO;

	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
	}
	
	protected override bool Statement() {
		Vector2 movementVector = _controller.adjustedGroundMovement;
		return movementVector.magnitude > OriginSO.threshold;
	}
}
