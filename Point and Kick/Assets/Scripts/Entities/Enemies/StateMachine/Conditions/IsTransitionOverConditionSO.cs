using Entities.Enemies;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsTransitionOverCondition", menuName = "State Machines/Conditions/Is Transition Over Condition")]
public class IsTransitionOverConditionSO : StateConditionSO<IsTransitionOverCondition> {
	public float minTransition = 0;
	public float maxTransition = 1;
}

public class IsTransitionOverCondition : Condition {
	protected new IsTransitionOverConditionSO OriginSO => (IsTransitionOverConditionSO)base.OriginSO;

	private EnemyController _enemyController;
	private float transitionTime;

	public override void Awake(StateMachine stateMachine) {
		_enemyController = stateMachine.GetComponent<EnemyController>();
		transitionTime = Random.Range(OriginSO.minTransition, OriginSO.maxTransition);
	}
	
	protected override bool Statement() {
		return Time.time - _enemyController.transitionTime >= transitionTime;
	}
	
	public override void OnStateEnter() {
		
	}
}
