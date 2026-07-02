using Entities.Enemies;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsCombatStateCondition", menuName = "State Machines/Conditions/Is Combat State Condition")]
public class IsCombatStateConditionSO : StateConditionSO<IsCombatStateCondition> {
	public StateSO combatState;
}

public class IsCombatStateCondition : Condition {
	private EnemyController _enemyController;
	private new IsCombatStateConditionSO OriginSO => (IsCombatStateConditionSO) base.OriginSO;

	public override void Awake(StateMachine stateMachine) {
		_enemyController = stateMachine.GetComponent<EnemyController>();
	}

	protected override bool Statement() {
		return _enemyController.moveSelected == OriginSO.combatState;
	}
}
