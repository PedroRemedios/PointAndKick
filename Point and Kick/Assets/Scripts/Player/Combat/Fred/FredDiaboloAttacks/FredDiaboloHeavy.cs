using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FredDiaboloHeavy : StateMachineBehaviour
{
    private PlayerAttack playerAttack;
    
    private void Awake() {
        playerAttack = GameObject.FindWithTag("Character Attack").GetComponent<PlayerAttack>();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        float colliderPosition = 2.70f;
        float colliderSize = 0.59f;
        playerAttack.SetAttackCollider(colliderPosition, colliderSize);
        playerAttack.SetAttackValues(attackDmg: 5, isHeavyAtk: false);

        animator.SetBool("Light Attack", false);
        animator.SetBool("Heavy Attack", false);
        animator.SetBool("Diabolo Light Attack", false);
        animator.SetBool("Diabolo Heavy Attack", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(animator.GetBool("tookDamage")) {
            animator.SetBool("Light Attack", false);
            animator.SetBool("Heavy Attack", false);
            animator.SetBool("Diabolo Light Attack", false);
            animator.SetBool("Diabolo Heavy Attack", false);
            animator.SetInteger("Combo", 0);
        }
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
