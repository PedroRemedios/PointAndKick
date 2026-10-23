using GameFlow.Interactions.InteractionVariants;

public interface InteractionVisitor
{
    void Visit(LookAtInteractionSO interaction);
    void Visit(DialogueInteractionSO interaction);
}
