using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState { NORMAL, ATTACKING, TRANSITION,
                         HIT, DOWN, DEAD, DESTROYED }
public class EnemyController : MonoBehaviour
{
    protected Rigidbody2D enemyRigidBody;
    [SerializeField] private EnemyState enemyState;

    public delegate void EnemyControllerDelegate(EnemyState state);
    public EnemyControllerDelegate onEnemyStateChange;

    [SerializeField] protected float totalHealth;
    [SerializeField] protected float damageVulnerability;
    protected float currentHealth;
    public float speed = 3;

    protected void SetEnemyState(EnemyState state) {
        enemyState = state;
        if (onEnemyStateChange != null)
            onEnemyStateChange(state);
    }

    public bool IsEnemyState(EnemyState state) {
        return enemyState == state;
    }

    public virtual void Move(Vector2 direction) {

    }

    public virtual void Attack() {
        
    }

    public virtual void DealDamage(float positionX, float sizeX, float damageDealt, bool heavyAttack) {
        
    } 

    public virtual void TakeDamage(float damageTaken, bool heavyAttack) {
        
    }

    public virtual void FinishAnimationState() {
        
    }
}
