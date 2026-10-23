using Entities.Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HasPressedLightAttack", menuName = "State Machines/Conditions/Has Pressed Light Attack")]
public class HasPressedLightAttackSO : StateConditionSO<HasPressedLightAttack> { }

public class HasPressedLightAttack : Condition {
	private PlayerController _playerController;
	public override void Awake(StateMachine stateMachine) {
		_playerController = stateMachine.GetComponent<PlayerController>();
	}
	
	protected override bool Statement() {
		return _playerController.lightAttackInput;
	}
}
