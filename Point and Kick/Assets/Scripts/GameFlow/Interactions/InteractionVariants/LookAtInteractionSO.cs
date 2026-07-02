using GameFlow.Interactions;
using UnityEngine;

namespace GameFlow.Interactions.InteractionVariants {
    [CreateAssetMenu(menuName = "Scriptable Objects/Interactions/Look At")]
    public class LookAtInteractionSO : InteractionSO
    {
        [Header ("Other Interactions that end this Interaction")]
        public InteractionTrigger[] interactionTriggers;

        public override void EnableInteraction(InteractionManager interactionManager) {
            interactionManager.EnableInteraction(this);
        }

        public override void Accept(InteractionVisitor visitor) {
            visitor.Visit(this);
        }

        public override DialogueInteractionSO GetDialogueFromInteraction(InteractionManager interactionManager) {
            return null;
        }
        
        public override void AddToManager(InteractionManager interactionManager) {
            interactionManager.Add(this);
        }
    }

    [System.Serializable]
    public class InteractionTrigger {
        [Header ("Next Interaction")]
        public LookAtInteractionSO nextLookAt;

        [Header ("Trigger")]
        public InteractionSO triggerInteraction;
    }
}