using System.Collections.Generic;
using Entities.Player;
using GameFlow.Interactions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    private CapsuleCollider2D playerCollider;
    private PlayerController playerController;
    private List<InteractionManager> nearbyInteractables = new List<InteractionManager>();
    [SerializeField] private InteractionPopupController interactionPopup;

    private bool HasMultipleInteractables() {
        return nearbyInteractables.Count > 1;
    }

    private bool HasOneInteractable() {
        return nearbyInteractables.Count == 1;
    }

    private bool HasNoInteractables() {
        return nearbyInteractables.Count == 0;
    }

    private void Start() {
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        PlayerInputActions playerInputActions = InputManager.Instance.GetPlayerInputActions();
        DefineInteractionActions(playerInputActions);
    }

    private void Update() {
        if (HasMultipleInteractables()) {
            // Doesn't happen yet, but when it does, might be a problem xD
        }
    }

    private void DefineInteractionActions(PlayerInputActions playerInputActions) {
        playerInputActions.Exploration.Interact.performed += CheckDialogue;
        playerInputActions.Exploration.LookAt.performed += CheckLookAt;
    }

    private void CheckDialogue(InputAction.CallbackContext callbackContext) {
        CheckInteraction(true);
    }

    private void CheckLookAt(InputAction.CallbackContext callbackContext) {
        CheckInteraction(false);
    }

    private void CheckInteraction(bool isDialogue) {
        if (!HasNoInteractables()) {
            InteractionManager closestInteractable = nearbyInteractables[0]; // TODO: Change this as well
            bool playerFacingRight = PlayerFacingInteractable(closestInteractable.gameObject.transform.position);
            interactionPopup.HideInteractionPopup();

            if (isDialogue) {
                InteractionGFX interactionGFX = nearbyInteractables[0].gameObject.GetComponent<InteractionGFX>();
                interactionGFX.UpdateNPCOrientation(playerFacingRight);
                nearbyInteractables[0].ListDialogues(playerFacingRight);
            }
            else
                nearbyInteractables[0].BeginLookAt();
        }
    }

    private bool PlayerFacingInteractable(Vector3 interactablePosition) {
        float xDistance = interactablePosition.x - gameObject.transform.position.x;
        float scaleDirection = playerController.CheckDirection(xDistance);

        return scaleDirection > 0;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        InteractionManager interactable = other.GetComponent<InteractionManager>();
        if (interactable != null) {
            nearbyInteractables.Add(interactable);
            if (HasOneInteractable())
                interactionPopup.ShowInteractionPopup(other.transform.position, interactable.interactionPopup);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        InteractionManager interactable = other.GetComponent<InteractionManager>();
        if (interactable != null) {
            nearbyInteractables.Remove(interactable);
            if (HasNoInteractables())
                interactionPopup.HideInteractionPopup();
            if (HasOneInteractable())
                interactionPopup.ShowInteractionPopup(other.transform.position, nearbyInteractables[0].interactionPopup);
        } 
    }
}
