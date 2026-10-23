using Entities;
using Entities.Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "CalculateKnockedTimeAction", menuName = "State Machines/Actions/Calculate Knocked Time Action")]
public class CalculateKnockedTimeActionSO : StateActionSO<CalculateKnockedTimeAction> { }

public class CalculateKnockedTimeAction : StateAction {
	private Controller _controller;
	private Animator _animator;

	public override void Awake(StateMachine stateMachine) {
		_controller = stateMachine.GetComponent<Controller>();
		_animator = _controller.spriteAnimator;
	}
	
	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		float acceleration = _controller.data.characterWeight * -9.81f;
		float airborneTime = -_controller.aerialMovement - Mathf.Sqrt(Mathf.Pow(_controller.aerialMovement, 2) - 2 * acceleration * _controller.spriteTransform.localPosition.y);
		
		_animator.SetFloat("airborneTime", _controller.data.knockedAnimationDuration * acceleration / airborneTime);
	}
}
