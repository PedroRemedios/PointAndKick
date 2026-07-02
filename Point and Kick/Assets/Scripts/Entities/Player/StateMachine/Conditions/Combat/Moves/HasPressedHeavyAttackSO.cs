using Entities.Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HasPressedHeavyAttack", menuName = "State Machines/Conditions/Has Pressed Heavy Attack")]
public class HasPressedHeavyAttackSO : StateConditionSO<HasPressedHeavyAttack> { }

public class HasPressedHeavyAttack : Condition {
	private PlayerController _playerController;
	public override void Awake(StateMachine stateMachine) {
		_playerController = stateMachine.GetComponent<PlayerController>();
	}
	
	protected override bool Statement() {
		return _playerController.heavyAttackInput;
	}
}
