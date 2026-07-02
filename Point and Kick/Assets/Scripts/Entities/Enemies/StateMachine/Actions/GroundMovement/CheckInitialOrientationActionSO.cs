using Entities.Enemies;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "CheckInitialOrientationAction", menuName = "State Machines/Actions/Check Initial Orientation Action")]
public class CheckInitialOrientationActionSO : StateActionSO<CheckInitialOrientationAction> { }

public class CheckInitialOrientationAction : StateAction {
	private EnemyController _enemyController;
	
	public override void Awake(StateMachine stateMachine) {
		_enemyController = stateMachine.GetComponent<EnemyController>();
	}

	public override void OnStateEnter() {
		if((_enemyController.isFacingLeft && _enemyController.characterTransform.position.x < _enemyController.playerTransform.position.x) || 
		   (!_enemyController.isFacingLeft && _enemyController.characterTransform.position.x > _enemyController.playerTransform.position.x)) {
			_enemyController.isFacingLeft = !_enemyController.isFacingLeft;
			_enemyController.characterTransform.localScale = new Vector3(-_enemyController.characterTransform.localScale.x, _enemyController.characterTransform.localScale.y, _enemyController.characterTransform.localScale.z);
		}
	}
	
	public override void OnUpdate() { }
}

