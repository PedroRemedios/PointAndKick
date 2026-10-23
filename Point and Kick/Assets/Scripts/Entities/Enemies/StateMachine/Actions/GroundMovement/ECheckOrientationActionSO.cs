using Entities.Enemies;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ECheckOrientationAction", menuName = "State Machines/Actions/ECheck Orientation Action")]
public class ECheckOrientationActionSO : StateActionSO<ECheckOrientationAction> { }

public class ECheckOrientationAction : StateAction {
	private EnemyController _enemyController;
	
	public override void Awake(StateMachine stateMachine) {
		_enemyController = stateMachine.GetComponent<EnemyController>();
	}
	
	public override void OnUpdate() {
		if((_enemyController.isFacingLeft && _enemyController.characterTransform.position.x < _enemyController.playerTransform.position.x) || 
		   (!_enemyController.isFacingLeft && _enemyController.characterTransform.position.x > _enemyController.playerTransform.position.x)) {
			_enemyController.isFacingLeft = !_enemyController.isFacingLeft;
			_enemyController.characterTransform.localScale = new Vector3(-_enemyController.characterTransform.localScale.x, _enemyController.characterTransform.localScale.y, _enemyController.characterTransform.localScale.z);
		}
	}
}
