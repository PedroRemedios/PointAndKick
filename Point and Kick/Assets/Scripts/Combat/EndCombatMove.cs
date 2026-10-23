using Entities;
using UnityEngine;

public class EndCombatMove : StateMachineBehaviour {
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Controller controller = animator.transform.parent.GetComponent<Controller>();
        controller.finishAnimation = true;
        Debug.Log("True Script");
    }
}
