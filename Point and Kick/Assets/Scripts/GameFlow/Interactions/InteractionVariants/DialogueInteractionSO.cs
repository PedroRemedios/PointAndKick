using UnityEngine;

namespace GameFlow.Interactions.InteractionVariants {
    [CreateAssetMenu(menuName = "Scriptable Objects/Interactions/Dialogue")]
    public class DialogueInteractionSO : InteractionSO {
        public override void EnableInteraction(InteractionManager interactionManager) {
            interactionManager.EnableInteraction(this);
        }

        public override void Accept(InteractionVisitor visitor) {
            visitor.Visit(this);
        }
        public override DialogueInteractionSO GetDialogueFromInteraction(InteractionManager interactionManager) {
            return this;
        }
        public override void AddToManager(InteractionManager interactionManager) {
            interactionManager.Add(this);
        }
    }
}
