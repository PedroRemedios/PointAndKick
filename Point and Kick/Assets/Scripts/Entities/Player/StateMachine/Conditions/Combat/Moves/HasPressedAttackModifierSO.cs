using Entities.Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HasPressedAttackModifier", menuName = "State Machines/Conditions/Has Pressed Attack Modifier")]
public class HasPressedAttackModifierSO : StateConditionSO<HasPressedAttackModifier> { }

public class HasPressedAttackModifier : Condition {
	private PlayerController _playerController;
	public override void Awake(StateMachine stateMachine) {
		_playerController = stateMachine.GetComponent<PlayerController>();
	}
	
	protected override bool Statement() {
		return _playerController.attackModifierInput;
	}
}
