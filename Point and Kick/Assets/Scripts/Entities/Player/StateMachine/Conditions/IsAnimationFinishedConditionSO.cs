using Entities;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsAnimationFinishedCondition", menuName = "State Machines/Conditions/Is Animation Finished Condition")]
public class IsAnimationFinishedConditionSO : StateConditionSO<IsAnimationFinishedCondition> {
}

public class IsAnimationFinishedCondition : Condition {
	private Controller _controller;
	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
	}
	
	protected override bool Statement() {
		return _controller.finishAnimation;
	}
}
