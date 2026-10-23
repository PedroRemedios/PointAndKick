using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TextboxController : MonoBehaviour
{
    [SerializeField] private Image speakerPortrait;
    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private TextMeshProUGUI narrationLine;

    [SerializeField] private InteractionChannelSO interactionChannel;
    private bool canReceiveInput = false;
    private NarrationLine currentLine = null;

    private void Awake() {
        interactionChannel.OnInteractionStart += InteractionStart;
        interactionChannel.OnInteractionEnd += InteractionEnd;
        interactionChannel.OnNarrationLineStart += NarrationLineStart;
        interactionChannel.OnNarrationLineEnd += NarrationLineEnd;
    }

    private void Start() {
        PlayerInputActions playerInputActions = InputManager.Instance.GetPlayerInputActions();
        playerInputActions.Interaction.Continue.started += ContinueInteraction;

        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        interactionChannel.OnInteractionStart -= InteractionStart;
        interactionChannel.OnInteractionEnd -= InteractionEnd;
        interactionChannel.OnNarrationLineStart -= NarrationLineStart;
        interactionChannel.OnNarrationLineEnd -= NarrationLineEnd;  
    }

    private void ContinueInteraction(InputAction.CallbackContext callbackContext) {
        if (canReceiveInput)
            interactionChannel.RequestNarrationLine(currentLine.nextLine);
    }

    private void NarrationLineStart(NarrationLine line) {
        speakerPortrait.sprite = line.dialogueSpeaker.speakerSprite;
        speakerName.text = line.dialogueSpeaker.speakerName;
        narrationLine.text = line.dialogueLine;

        canReceiveInput = true;
        currentLine = line;
    }

    private void NarrationLineEnd(NarrationLine line) {
        currentLine = null;
        canReceiveInput = false;

        speakerPortrait.sprite = null;
        speakerName.text = "";
        narrationLine.text = "";
    }

    private void InteractionStart(InteractionSO interaction) {
        gameObject.SetActive(true);
    }

    private void InteractionEnd(InteractionSO interaction) {
        gameObject.SetActive(false);
    }
}
