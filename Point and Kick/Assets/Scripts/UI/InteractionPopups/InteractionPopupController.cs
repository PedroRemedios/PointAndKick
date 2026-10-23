using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPopupController : MonoBehaviour
{
    public GameObject dialoguePopup;
    public GameObject lookAtPopup;
    
    private void Awake() {
        HideInteractionPopup();
    }

    public void ShowInteractionPopup(Vector3 interactablePosition, InteractionPopupSO popupInformation) {
        gameObject.transform.position = interactablePosition + popupInformation.popupOffset;

        gameObject.SetActive(true);
        dialoguePopup.SetActive(popupInformation.hasDialogue);
        lookAtPopup.SetActive(popupInformation.hasDialogue);
    }

    public void HideInteractionPopup() {
        gameObject.SetActive(false);
    }
}
