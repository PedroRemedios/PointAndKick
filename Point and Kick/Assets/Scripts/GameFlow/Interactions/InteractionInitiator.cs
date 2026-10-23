using UnityEngine;

public class InteractionInitiator : MonoBehaviour
{
    [SerializeField] private InteractionChannelSO interactionChannel = default;
    private InteractionController interactionController;

    private void Awake() {
        interactionController = new InteractionController();

        interactionController.OnInteractionStart += OnInteractionStart;
        interactionController.OnInteractionEnd += OnInteractionEnd;
        interactionController.OnNarrationLineStart += interactionChannel.NarrationLineStart;
        interactionController.OnNarrationLineEnd += interactionChannel.NarrationLineEnd;

        interactionChannel.OnInteractionRequested += interactionController.StartInteraction;
        interactionChannel.OnNarrationLineRequested += interactionController.StartNarrationLine;
    }

    private void OnDestroy() {
        interactionChannel.OnNarrationLineRequested -= interactionController.StartNarrationLine;
        interactionChannel.OnInteractionRequested -= interactionController.StartInteraction;

        interactionController.OnNarrationLineEnd -= interactionChannel.NarrationLineEnd;
        interactionController.OnNarrationLineStart -= interactionChannel.NarrationLineStart;
        interactionController.OnInteractionEnd -= OnInteractionEnd;
        interactionController.OnInteractionStart -= OnInteractionStart;

        interactionController = null;
    }

    private void OnInteractionStart(InteractionSO interaction) {
        interactionChannel.InteractionStart(interaction);
    }

    private void OnInteractionEnd(InteractionSO interaction) {
        interactionChannel.InteractionEnd(interaction);
        
        if (interaction.eventTrigger != null)
            interaction.eventTrigger.RaiseEvent();
    }
}
