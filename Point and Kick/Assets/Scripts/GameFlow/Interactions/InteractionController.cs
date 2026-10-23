public class InteractionException : System.Exception {
    public InteractionException(string message) : base(message) { }
}

public class InteractionController {
    public delegate void InteractionCallback(InteractionSO interaction);
    public delegate void NarrationLineCallback(NarrationLine line);

    public InteractionCallback OnInteractionStart;
    public InteractionCallback OnInteractionEnd;
    public NarrationLineCallback OnNarrationLineStart;
    public NarrationLineCallback OnNarrationLineEnd;

    private InteractionSO currentInteraction;
    private NarrationLine currentLine;

    public void StartInteraction(InteractionSO interaction) {
        if (currentInteraction == null) {
            currentInteraction = interaction;
            OnInteractionStart?.Invoke(currentInteraction);
            StartNarrationLine(currentInteraction.firstLine);
        }
        else
            throw new InteractionException("Can't start an interaction when another is already running.");
    }

    public void EndInteraction(InteractionSO interaction) {
        if (currentInteraction == interaction) {
            StopNarrationLine(currentLine);
            OnInteractionEnd?.Invoke(currentInteraction);
            currentInteraction = null;
        }
        else
            throw new InteractionException("Trying to stop an interaction that ins't running.");
    }

    private bool CanStartLine(NarrationLine line) {
        return (currentLine == null || line == null || currentLine.IsFollowedByLine(line));
    }

    public void StartNarrationLine(NarrationLine line) {
        if (CanStartLine(line)) {
            StopNarrationLine(currentLine);

            currentLine = line;

            if (currentLine != null)
                OnNarrationLineStart?.Invoke(currentLine);
            else
                EndInteraction(currentInteraction);
        }
        else
            throw new InteractionException("Failed to start interaction line.");
    }

    private void StopNarrationLine(NarrationLine line) {
        if (currentLine == line) {
            if (currentLine != null) {
                OnNarrationLineEnd?.Invoke(currentLine);
                currentLine = null;
            }
        }
        else
            throw new InteractionException("Trying to stop an interaction line that ins't running.");
    }
}