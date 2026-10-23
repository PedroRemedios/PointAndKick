using Entities.HealthStatus.Damageables;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "CheckForParryAction", menuName = "State Machines/Actions/Check For Parry Action")]
public class CheckForParryActionSO : StateActionSO<CheckForParryAction> { }

public class CheckForParryAction : StateAction {
	private Damageable _damageable;
	public override void Awake(StateMachine stateMachine) {
		_damageable = stateMachine.GetComponent<Damageable>();
	}
	
	public override void OnUpdate() {
		if (_damageable.takingHeavyAttack || _damageable.takingLightAttack) {
			Damageable _attackerDamageable = _damageable.currentAttackerTransform.parent.gameObject.GetComponent<Damageable>();
			_attackerDamageable.isStunned = true;
			
			_damageable.takingLightAttack = false;
			_damageable.takingHeavyAttack = false;
			_damageable.resumingKnocked = false;
			_damageable.isStunned = false;

		}
	}
}
