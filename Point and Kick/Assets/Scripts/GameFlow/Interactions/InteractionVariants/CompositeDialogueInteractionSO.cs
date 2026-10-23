using UnityEngine;

namespace GameFlow.Interactions.InteractionVariants {
    [CreateAssetMenu(menuName = "Scriptable Objects/Interactions/Composite Dialogue")]
    public class CompositeDialogueInteractionSO : InteractionSO {
        [Header ("Ordered List of Dialogues")]
        public DialogueInteractionSO[] dialogueSequence;

        public override void EnableInteraction(InteractionManager interactionManager) {
            interactionManager.EnableInteraction(this);
        }

        public override void Accept(InteractionVisitor visitor) {
            throw new System.NotImplementedException();
        }

        public override DialogueInteractionSO GetDialogueFromInteraction(InteractionManager interactionManager) {
            return interactionManager.ChooseDialogueFromComposite(this);
        }
        
        public override void AddToManager(InteractionManager interactionManager) {
            interactionManager.Add(this);
        }
    }
}
