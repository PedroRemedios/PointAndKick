using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FredUppercut : StateMachineBehaviour
{
    private Animator diaboloAnimator;
    private void Awake() {
        diaboloAnimator = GameObject.FindWithTag("Character Weapon").GetComponent<Animator>();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
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
        if(animator.GetBool("Heavy Attack") && (animator.GetInteger("Combo") == 3 || animator.GetInteger("Combo") == 4)) {
            animator.Play("Fred Headbutt");
        }
        else if(animator.GetBool("Diabolo Heavy Attack") && animator.GetInteger("Combo") == 4) {
            animator.Play("Fred Diabolo Heavy");
            diaboloAnimator.Play("Diabolo Heavy");
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
