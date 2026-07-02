using Entities.Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsComboCondition", menuName = "State Machines/Conditions/Is Combo Condition")]
public class IsComboConditionSO : StateConditionSO<IsComboCondition> {
	public int requiredCombo;
}

public class IsComboCondition : Condition {
	private PlayerController _playerController;
	private new IsComboConditionSO OriginSO => (IsComboConditionSO) base.OriginSO;

	public override void Awake(StateMachine stateMachine) {
		_playerController = stateMachine.GetComponent<PlayerController>();
	}
	
	protected override bool Statement() {
		return _playerController.attackCombo == OriginSO.requiredCombo;
	}
}
