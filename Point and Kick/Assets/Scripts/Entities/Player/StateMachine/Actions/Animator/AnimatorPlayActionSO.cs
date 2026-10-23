using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AnimatorPlayAction", menuName = "State Machines/Actions/Animator Play Action")]
public class AnimatorPlayActionSO : StateActionSO<AnimatorPlayAction> {
	public string animationName;
}

public class AnimatorPlayAction : StateAction {
	private Animator _animator;
	private new AnimatorPlayActionSO OriginSO => (AnimatorPlayActionSO) base.OriginSO;

	public override void Awake(StateMachine stateMachine) {
		_animator = stateMachine.GetChildComponent<Animator>();
	}
	
	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		_animator.Play(OriginSO.animationName);
	}
}
