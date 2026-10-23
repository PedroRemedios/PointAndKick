using Entities.Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsHoldingGuardDodgeCondition", menuName = "State Machines/Conditions/Is Holding Guard Dodge Condition")]
public class IsHoldingGuardDodgeConditionSO : StateConditionSO<IsHoldingGuardDodgeCondition> { }

public class IsHoldingGuardDodgeCondition : Condition {
	private PlayerController _playerController;
	public override void Awake(StateMachine stateMachine) {
		_playerController = stateMachine.GetComponent<PlayerController>();
	}
	
	protected override bool Statement() {
		return _playerController.guardDodgeInput;
	}
}
