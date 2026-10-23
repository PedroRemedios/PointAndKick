using Entities.Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsRunningCondition", menuName = "State Machines/Conditions/Is Running Condition")]
public class IsRunningConditionSO : StateConditionSO<IsRunningCondition> { }

public class IsRunningCondition : Condition {
	private PlayerController _playerController;

	public override void Awake(StateMachine stateMachine) {
		_playerController = stateMachine.GetComponent<PlayerController>();
	}
	
	protected override bool Statement() {
		return _playerController.isRunning;
	}
}
