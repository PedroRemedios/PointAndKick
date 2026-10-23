using Entities.HealthStatus.Damageables;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "InflictDamageAction", menuName = "State Machines/Actions/Inflict Damage Action")]
public class InflictDamageActionSO : StateActionSO<InflictDamageAction> { }

public class InflictDamageAction : StateAction {
	private Damageable _damageable;
	public override void Awake(StateMachine stateMachine) {
		_damageable = stateMachine.GetComponent<Damageable>();
	}
	
	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		_damageable.InflictDamage();
		_damageable.UpdateUI();
	}
}
