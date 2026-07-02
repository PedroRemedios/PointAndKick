using UnityEngine;

namespace Effects {
    public class EndOfEffect : StateMachineBehaviour {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            Destroy(animator.gameObject);
        }
    }
}