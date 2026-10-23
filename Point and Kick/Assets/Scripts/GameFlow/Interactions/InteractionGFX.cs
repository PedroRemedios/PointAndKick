using UnityEngine;
using Cinemachine;
using GameFlow.Interactions;
using GameFlow.Interactions.InteractionVariants;

public class InteractionGFX : MonoBehaviour
{
    [SerializeField] private bool isFacingLeft;
    [SerializeField] private bool isTurnable;
    [SerializeField] private InteractionChannelSO interactionChannel = default;
    [SerializeField] private NarrationSpeaker narrationSpeaker;
    [SerializeField] private CinemachineVirtualCamera cinemachineNPC;
    [SerializeField] private Animator cinemachineAnimator;
    
    private Animator animator;
    private InteractionManager interactionManager;

    private void Awake() {
        animator = GetComponent<Animator>();
        interactionManager = GetComponent<InteractionManager>();

        interactionChannel.OnNarrationLineStart += NarrationLineStart;
        interactionChannel.OnNarrationLineEnd += NarrationLineEnd;
        interactionChannel.OnInteractionStart += InteractionStart;
        interactionChannel.OnInteractionEnd += InteractionEnd;
    }

    private void OnDestroy() {
        interactionChannel.OnNarrationLineStart -= NarrationLineStart;
        interactionChannel.OnNarrationLineEnd -= NarrationLineEnd;  
        interactionChannel.OnInteractionStart -= InteractionStart;  
        interactionChannel.OnInteractionEnd -= InteractionEnd;
    }

    private void NarrationLineStart(NarrationLine narrationLine) {
        if (narrationLine.dialogueSpeaker.speakerName == narrationSpeaker.speakerName)
            animator.SetBool("speaking", true);
    }

    private void NarrationLineEnd(NarrationLine narrationLine) {
        if (narrationLine.dialogueSpeaker.speakerName == narrationSpeaker.speakerName)
            animator.SetBool("speaking", false);
    }

    private void InteractionStart(InteractionSO interaction) {
        if (interaction.actor != narrationSpeaker) return;
        
        cinemachineNPC.m_Follow = gameObject.transform;
        if (interaction is LookAtInteractionSO) {
            cinemachineAnimator.Play("NPC Camera");
        }
    }

    private void InteractionEnd(InteractionSO interaction) {
        if (interaction.actor != narrationSpeaker) return;
        
        if (interaction is LookAtInteractionSO)
            cinemachineAnimator.Play("Normal Camera");
    }

    public void UpdateNPCOrientation(bool shouldFaceLeft) {
        if (isTurnable)
            if ((shouldFaceLeft && !isFacingLeft) || (!shouldFaceLeft && isFacingLeft)) {
                isFacingLeft = !isFacingLeft;
                transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
            }
    }
}
