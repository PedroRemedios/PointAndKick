using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Vector2 attackColliderPosition;
    private Vector2 attackColliderSize;
    private float attackDamage;
    private bool isHeavyAttack;
    private bool debugBox = true;

    public void Attack()
    {
        Vector2 attackGlobalPosition = (Vector2) transform.position + attackColliderPosition * transform.lossyScale;
        RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(attackGlobalPosition, attackColliderSize, 0f, Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Enemy GFX"));

        foreach(RaycastHit2D raycastHit in raycastHits) {
            EnemyController enemyController = raycastHit.collider.gameObject.transform.parent.gameObject.GetComponent<EnemyController>();
            enemyController.TakeDamage(attackDamage, isHeavyAttack);
        }

        if(debugBox) {
            Debug.DrawRay(attackGlobalPosition - attackColliderSize / 2, Vector2.right * attackColliderSize);
            Debug.DrawRay(attackGlobalPosition - attackColliderSize / 2, Vector2.up * attackColliderSize);
            Debug.DrawRay(attackGlobalPosition + attackColliderSize / 2, Vector2.left * attackColliderSize);
            Debug.DrawRay(attackGlobalPosition + attackColliderSize / 2, Vector2.down * attackColliderSize);
        }
    }

    public void SetAttackCollider(float positionX, float sizeX) {
        attackColliderPosition = new Vector2(positionX, 0f);
        attackColliderSize = new Vector2(sizeX, transform.parent.GetComponent<CapsuleCollider2D>().bounds.extents.y * 2);
    }

    public void SetAttackValues(float attackDmg, bool isHeavyAtk) {
        attackDamage = attackDmg;
        isHeavyAttack = isHeavyAtk;
    }
}
