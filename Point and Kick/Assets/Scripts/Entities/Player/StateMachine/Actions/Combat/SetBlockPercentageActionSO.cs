using Entities.HealthStatus.Damageables;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SetBlockPercentageAction", menuName = "State Machines/Actions/Set Block Percentage Action")]
public class SetBlockPercentageActionSO : StateActionSO<SetBlockPercentageAction> {
	public float blockPercentage;
}

public class SetBlockPercentageAction : StateAction {
	private PlayerDamageable _playerDamageable;
	private SetBlockPercentageActionSO _originSO => (SetBlockPercentageActionSO) base.OriginSO;

	public override void Awake(StateMachine stateMachine) {
		_playerDamageable = stateMachine.GetComponent<PlayerDamageable>();
	}
	
	public override void OnUpdate() { }
	
	public override void OnStateEnter() {
		_playerDamageable.blockPercentage = _originSO.blockPercentage;
	}
}
