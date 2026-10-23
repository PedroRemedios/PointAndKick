using Entities.HealthStatus.Damageables;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HasResumedKnocked", menuName = "State Machines/Conditions/Has Resumed Knocked")]
public class HasResumedKnockedSO : StateConditionSO<HasResumedKnocked> { }

public class HasResumedKnocked : Condition {
	private Damageable _playerDamageable;
	public override void Awake(StateMachine stateMachine) {
		_playerDamageable = stateMachine.GetComponent<Damageable>();
	}
	
	protected override bool Statement() {
		return _playerDamageable.resumingKnocked;
	}
}
