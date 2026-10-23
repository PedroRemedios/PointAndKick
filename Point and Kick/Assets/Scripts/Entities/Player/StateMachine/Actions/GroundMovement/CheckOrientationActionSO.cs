using Entities.Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "CheckOrientationAction", menuName = "State Machines/Actions/Check Orientation Action")]
public class CheckOrientationActionSO : StateActionSO<CheckOrientationAction> {
	public bool triggerTurn;
}

public class CheckOrientationAction : StateAction {
	private PlayerController _playerController;
	private new CheckOrientationActionSO OriginSO => (CheckOrientationActionSO) base.OriginSO;
	
	public override void Awake(StateMachine stateMachine) {
		_playerController = stateMachine.GetComponent<PlayerController>();
	}
	
	public override void OnUpdate() {
		if((_playerController.adjustedGroundMovement.x < 0 && _playerController.isFacingRight) || 
		   (_playerController.adjustedGroundMovement.x > 0 && !_playerController.isFacingRight)) {
			_playerController.isFacingRight = !_playerController.isFacingRight;
			_playerController.scaleDirection *= -1;
			_playerController.orientationChannel.RaiseEvent(_playerController.isFacingRight);
			if (OriginSO.triggerTurn)
				_playerController.spriteAnimator.SetTrigger("turning");
		}
	}
}
