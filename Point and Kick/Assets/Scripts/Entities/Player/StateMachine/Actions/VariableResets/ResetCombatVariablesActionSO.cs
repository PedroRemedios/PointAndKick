using Entities.Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ResetCombatVariablesAction",
	menuName = "State Machines/Actions/Reset Combat Variables Action")]
public class ResetCombatVariablesActionSO : StateActionSO<ResetCombatVariablesAction> {
	public bool resetCombo;
}

public class ResetCombatVariablesAction : StateAction {
	private PlayerController _playerController;
	private new ResetCombatVariablesActionSO OriginSO => (ResetCombatVariablesActionSO) base.OriginSO;
	
	public override void Awake(StateMachine stateMachine) {
		_playerController = stateMachine.GetComponent<PlayerController>();
	}
	
	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		_playerController.lightAttackInput = false;
		_playerController.heavyAttackInput = false;

		if (OriginSO.resetCombo) {
			_playerController.attackCombo = 0;
			_playerController.attackInProgress = false;
		}
		else {
			_playerController.attackCombo++;
			_playerController.attackInProgress = true;
		}
	}
}
