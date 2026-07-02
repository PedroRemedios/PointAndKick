using Entities.Enemies;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SetInitialHorizontalMovementAction", menuName = "State Machines/Actions/Set Initial Horizontal Movement Action")]
public class SetInitialHorizontalMovementActionSO : StateActionSO<SetInitialHorizontalMovementAction> { }

public class SetInitialHorizontalMovementAction : StateAction {
	private EnemyController _enemyController;
	public override void Awake(StateMachine stateMachine) {
		_enemyController = stateMachine.GetComponent<EnemyController>();
	}
	
	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		_enemyController.rawGroundMovement.x = - _enemyController.characterTransform.lossyScale.x;
		_enemyController.rawGroundMovement.y = 0;
	}
}
