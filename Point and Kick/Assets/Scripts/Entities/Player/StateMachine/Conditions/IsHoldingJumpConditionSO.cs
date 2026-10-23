using Entities.Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsHoldingJumpCondition", menuName = "State Machines/Conditions/Is Holding Jump Condition")]
public class IsHoldingJumpConditionSO : StateConditionSO<IsHoldingJumpCondition> { }

public class IsHoldingJumpCondition : Condition {
	private PlayerController _playerController;
	public override void Awake(StateMachine stateMachine) {
		_playerController = stateMachine.GetComponent<PlayerController>();
	}
	
	protected override bool Statement() {
		return _playerController.jumpInput;
	}
}
