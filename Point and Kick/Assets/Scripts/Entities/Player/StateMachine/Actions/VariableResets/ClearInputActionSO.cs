using Entities.Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ClearInputAction", menuName = "State Machines/Actions/Clear Input Action")]
public class ClearInputActionSO : StateActionSO<ClearInputAction> { }

public class ClearInputAction : StateAction {
	private PlayerController _playerController;
	
	public override void Awake(StateMachine stateMachine) {
		_playerController = stateMachine.GetComponent<PlayerController>();
	}
	
	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		_playerController.jumpInput = false;
		_playerController.guardDodgeInput = false;
	}
}
