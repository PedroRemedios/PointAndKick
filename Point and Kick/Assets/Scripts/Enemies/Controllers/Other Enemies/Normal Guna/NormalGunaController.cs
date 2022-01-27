using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalGunaController : EnemyController
{
    private CapsuleCollider2D enemyCollider;
    private Animator animator;

    private Vector2 moveDirection;
    private Text textCoiso;

    private void Awake() {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<CapsuleCollider2D>();
        SetEnemyState(EnemyState.NORMAL);

        moveDirection = new Vector2(0, 0);
        currentHealth = totalHealth;
        damageVulnerability = 1;
        totalHealth = 200;
    }

    private void Start() {
        animator = gameObject.transform.Find("Normal Guna GFX").GetComponent<Animator>();
        textCoiso = GameObject.FindWithTag("HP").GetComponent<Text>();
    }

    private void Update() {
        
    }

    private void FixedUpdate() {
        if(IsEnemyState(EnemyState.NORMAL))
            enemyRigidBody.position += moveDirection * speed * Time.deltaTime;
    }

    public override void Move(Vector2 direction) {
        moveDirection = direction;
        animator.SetFloat("Speed", moveDirection.magnitude);
    }

    public override void Attack() {
        if(IsEnemyState(EnemyState.NORMAL)) {
            SetEnemyState(EnemyState.ATTACKING);

            float attackValue = Random.Range(0f, 1f);
            if (attackValue < 0.7) {
                animator.SetTrigger("lightAttack");
            }
            else {
                animator.SetTrigger("heavyAttack");
            }
        }
    }

    public override void DealDamage(float positionX, float sizeX, float damageDealt, bool heavyAttack) {
        Vector2 attackColliderPosition = new Vector2(positionX, 0f);
        Vector2 attackColliderSize = new Vector2(sizeX, enemyCollider.size.y);

        Vector2 attackGlobalPosition = (Vector2) transform.position + attackColliderPosition * transform.lossyScale;
        RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(attackGlobalPosition, attackColliderSize, 0f, Vector2.zero, 0f, 1 << LayerMask.NameToLayer("Character"));

        foreach(RaycastHit2D raycastHit in raycastHits) {
            PlayerCombat playerCombat = raycastHit.collider.gameObject.GetComponent<PlayerCombat>();
            playerCombat.TakeDamage(damageDealt, heavyAttack);
        }
    }

    public override void TakeDamage(float damageTaken, bool heavyAttack) {
        if(!IsEnemyState(EnemyState.DOWN) && !IsEnemyState(EnemyState.DEAD)) {
            currentHealth -= damageTaken * damageVulnerability;

            if(currentHealth <= 0) {
                currentHealth = 0;

                animator.SetBool("isDead", true);
                SetEnemyState(EnemyState.DEAD);
            }
            else {
                if(!heavyAttack) {
                    animator.SetTrigger("tookLightDamage");
                    SetEnemyState(EnemyState.HIT);
                }
                else {
                    animator.SetTrigger("tookHeavyDamage");
                    SetEnemyState(EnemyState.DOWN);
                }
            }

            textCoiso.text = "GUNA HP: " + currentHealth + " / 200";
        }
    }

    // TODO: Change name
    public override void FinishAnimationState() {
        if (IsEnemyState(EnemyState.DEAD)) {
            SetEnemyState(EnemyState.DESTROYED);
            Destroy(gameObject);
        }
        else {
            SetEnemyState(EnemyState.TRANSITION);
            Invoke("FinishTransition", 1f);
        }
    }

    private void FinishTransition() {
        if (IsEnemyState(EnemyState.TRANSITION))
            SetEnemyState(EnemyState.NORMAL);
    }
}
