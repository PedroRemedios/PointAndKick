using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGunaGFX : MonoBehaviour
{
    EnemyController enemyController;

    void Start() {
        enemyController = transform.parent.gameObject.GetComponent<EnemyController>();
    }

    void Update() {
        
    }

    void FinishAnimationState() {
        enemyController.FinishAnimationState();
    }

    void LightAttack() {
        enemyController.DealDamage(positionX: -0.87f, sizeX: 1.25f, damageDealt: 15f, heavyAttack: false);
    }

    void HeavyAttack() {
        enemyController.DealDamage(positionX: -0.65f, sizeX: 1.52f, damageDealt: 70f, heavyAttack: true);
    }
}
