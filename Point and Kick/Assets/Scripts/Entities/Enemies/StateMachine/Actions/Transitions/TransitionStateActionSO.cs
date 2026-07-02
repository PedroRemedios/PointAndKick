using Entities.Enemies;
using Entities.Enemies.Pathfinding;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "TransitionStateAction", menuName = "State Machines/Actions/Transition State Action")]
public class TransitionStateActionSO : StateActionSO<TransitionStateAction> { }

public class TransitionStateAction : StateAction {
	private EnemyController _enemyController;
	private EnemyAI _enemyAI;
	public override void Awake(StateMachine stateMachine) {
		_enemyController = stateMachine.GetComponent<EnemyController>();
		_enemyAI = stateMachine.GetComponent<EnemyAI>();
	}
	
	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		_enemyController.transitionTime = Time.time;
	}
}
