using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FredPunchTwo : StateMachineBehaviour
{
    private PlayerAttack playerAttack;
    
    private void Awake() {
        playerAttack = GameObject.FindWithTag("Character Attack").GetComponent<PlayerAttack>();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        float colliderPosition = 1.19f;
        float colliderSize = 1.21f;
        playerAttack.SetAttackCollider(colliderPosition, colliderSize);
        playerAttack.SetAttackValues(attackDmg: 15, isHeavyAtk: false);

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
        if(animator.GetBool("Light Attack") && animator.GetInteger("Combo") == 3) {
            animator.Play("Fred Hook");
        }
        else if(animator.GetBool("Heavy Attack") && animator.GetInteger("Combo") == 3) {
            animator.Play("Fred Uppercut");
        }
        else {
            PlayerManager.Instance.SetPlayerState(PlayerState.IDLING);
            
            animator.SetBool("Light Attack", false);
            animator.SetBool("Heavy Attack", false);
            animator.SetBool("Diabolo Light Attack", false);
            animator.SetBool("Diabolo Heavy Attack", false);
            animator.SetInteger("Combo", 0);
        }
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
