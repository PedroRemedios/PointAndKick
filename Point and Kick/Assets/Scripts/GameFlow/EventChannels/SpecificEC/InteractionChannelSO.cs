using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Interactions/Interaction Channel")]
public class InteractionChannelSO : ScriptableObject
{
    public delegate void InteractionCallback(InteractionSO interaction);
    public InteractionCallback OnInteractionRequested;
    public InteractionCallback OnInteractionStart;
    public InteractionCallback OnInteractionEnd;

    public delegate void NarrationLineCallback(NarrationLine line);
    public NarrationLineCallback OnNarrationLineRequested;
    public NarrationLineCallback OnNarrationLineStart;
    public NarrationLineCallback OnNarrationLineEnd;

    public void RequestInteraction(InteractionSO interaction) {
        OnInteractionRequested?.Invoke(interaction);
    }

    public void InteractionStart(InteractionSO interaction) {
        OnInteractionStart?.Invoke(interaction);
    }

    public void InteractionEnd(InteractionSO interaction) {
        OnInteractionEnd?.Invoke(interaction);
    }

    public void RequestNarrationLine(NarrationLine line) {
        OnNarrationLineRequested?.Invoke(line);
    }

    public void NarrationLineStart(NarrationLine line) {
        OnNarrationLineStart?.Invoke(line);
    }

    public void NarrationLineEnd(NarrationLine line) {
        OnNarrationLineEnd?.Invoke(line);
    }
}
