using System;
using GameFlow.EventChannels.GeneralEC;
using GameFlow.Interactions;
using GameFlow.Interactions.InteractionVariants;
using GameFlow.Progression.Conditions;
using GameFlow.Questing;
using UnityEngine;

public abstract class InteractionSO : ScriptableObject
{
    public delegate void InteractionStatus(InteractionSO interaction);
    public event InteractionStatus OnInteractionEnabled;
    public event InteractionStatus OnInteractionDisabled;
    
    [Header ("Interacted Actor")]
    public NarrationSpeaker actor;
    
    [Header ("Starting Narration Line")]
    public NarrationLine firstLine;

    [Header("Quest Progression")] 
    public TaskConditionSO interactionEnabler;
    public TaskConditionSO interactionDisabler;

    [Header("End of Interaction Event Trigger")]
    public VoidEventChannelSO eventTrigger;

    private void OnEnable() {
        if (interactionEnabler != null)
            interactionEnabler.OnConditionVerified += EnableInteraction;
        
        if (interactionDisabler != null)
            interactionDisabler.OnConditionVerified += DisableInteraction;
    }

    private void OnDisable() {
        if (interactionEnabler != null)
            interactionEnabler.OnConditionVerified -= EnableInteraction;
        
        if (interactionDisabler != null)
            interactionDisabler.OnConditionVerified -= DisableInteraction;
    }

    private void EnableInteraction() {
        OnInteractionEnabled?.Invoke(this);
    }

    private void DisableInteraction() {
        OnInteractionDisabled?.Invoke(this);
    }

    public abstract DialogueInteractionSO GetDialogueFromInteraction(InteractionManager interactionManager);
    public abstract void AddToManager(InteractionManager interactionManager);
    public abstract void EnableInteraction(InteractionManager interactionManager);
    public abstract void Accept(InteractionVisitor visitor);
}