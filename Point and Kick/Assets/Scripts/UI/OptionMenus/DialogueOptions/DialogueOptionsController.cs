using System;
using System.Collections.Generic;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.OptionMenus.DialogueOptions {
    public class DialogueOptionsController : MonoBehaviour {
        [SerializeField] private DialogueOptionsChannelSO dialogueOptionsChannel;
        [SerializeField] private DialogueOption[] optionBoxes;

        private List<OptionInterface> interactions = new List<OptionInterface>();
        private NarrationSpeaker currentActor;
        private int currentOptionIndex = 0;
        private bool isSelecting = false;

        private void Awake() {
            dialogueOptionsChannel.OnOptionsMenuListed += DialoguesListed;
        }

        private void DialoguesListed(DialogueOptionsArgs value) {
            interactions = value.GetOptionInteractions();
            currentOptionIndex = 0;
            currentActor = value.GetActor();

            SetupDialogueOptions(value.IsPositionedRight(), currentActor.offsets.x);
            gameObject.transform.position = value.GetOptionPositions() + new Vector3(0, currentActor.offsets.y, 0);
            gameObject.SetActive(true);
        }

        private void SetupDialogueOptions(bool isRight, float horizontalOffset) {
            isSelecting = true;
            DialogueOption upperBox = optionBoxes[0];
            upperBox.SetPosition(isRight, horizontalOffset);
            upperBox.Select();
            upperBox.SetText(interactions[0].GetName());

            int numberOfOptions = interactions.Count;
            if (numberOfOptions > 1) {
                DialogueOption middleBox = optionBoxes[1];
                middleBox.SetPosition(isRight, horizontalOffset);
                middleBox.Unselect();
                middleBox.SetText(interactions[1].GetName());
                
                if (numberOfOptions > 2) {
                    DialogueOption lowerBox = optionBoxes[2];
                    lowerBox.SetPosition(isRight, horizontalOffset);
                    lowerBox.Unselect();
                    lowerBox.SetText(interactions[2].GetName());
                }
            }
        }

        private void Start() {
            PlayerInputActions playerInputActions = InputManager.Instance.GetPlayerInputActions();
            playerInputActions.Interaction.Continue.performed += SelectDialogue;
            playerInputActions.Interaction.NavigateUp.performed += HighlightOptionAbove;
            playerInputActions.Interaction.NavigateDown.performed += HighlightOptionBelow;
            
            gameObject.SetActive(false);
        }

        private void SelectDialogue(InputAction.CallbackContext obj) {
            if (!isSelecting) return;
            
            InteractionSO dialogue = interactions[currentOptionIndex].SelectOption();
            dialogueOptionsChannel.SelectOption(dialogue, currentActor);
                
            optionBoxes[0].ResetOption();
            optionBoxes[1].ResetOption();
            optionBoxes[2].ResetOption();
            gameObject.SetActive(false);
            isSelecting = false;

        }
        
        private void HighlightOptionAbove(InputAction.CallbackContext obj) {
            if (!isSelecting) return;
            if (currentOptionIndex == 0) return;

            int currentIndex = currentOptionIndex;
            currentOptionIndex--;
            if (currentIndex == interactions.Count - 1 || currentIndex == 1)
                ChangeDialogueBoxes(true);
            else
                ChangeDialogueText();
        }

        private void HighlightOptionBelow(InputAction.CallbackContext obj) {
            if (!isSelecting) return;
            if (currentOptionIndex == interactions.Count - 1) return;
            
            int currentIndex = currentOptionIndex;
            currentOptionIndex++;
            if (currentIndex == interactions.Count - 2 || currentIndex == 0)
                ChangeDialogueBoxes(false);
            else
                ChangeDialogueText();
        }

        private void ChangeDialogueBoxes(bool isAbove) {
            if (currentOptionIndex == 0) {
                optionBoxes[0].Select();
                optionBoxes[1].Unselect();
            }
            else if (currentOptionIndex == interactions.Count - 1 && currentOptionIndex != 1) {
                optionBoxes[2].Select();
                optionBoxes[1].Unselect();
            }
            else {
                optionBoxes[1].Select();
                if (isAbove)
                    optionBoxes[2].Unselect();
                else
                    optionBoxes[0].Unselect();
            }
        }

        private void ChangeDialogueText() {
            optionBoxes[0].SetText(interactions[currentOptionIndex - 1].GetName());
            optionBoxes[1].SetText(interactions[currentOptionIndex].GetName());
            optionBoxes[2].SetText(interactions[currentOptionIndex + 1].GetName());
        }
    }
}