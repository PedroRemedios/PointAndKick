using GameFlow.EventChannels.SpecificEC;
using System;
using System.Collections;
using System.Collections.Generic;
using UI.OptionMenus.DialogueOptions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.OptionMenus.DialogOptions {
    public class QuickAccessMenu : MonoBehaviour
    {
        [SerializeField] private DialogueOptionsChannelSO dialogueOptionsChannel;
        [SerializeField] private DialogueOption[] optionBoxes;

        private List<String> interactions = new List<String>();
        private NarrationSpeaker currentActor;
        private int currentOptionIndex = 0;
        private bool isSelecting = false;
        public Transform playerTransform;

        private void Awake()
        {
            
        }

        private void SetupDialogueOptions(bool isRight, float horizontalOffset)
        {
            isSelecting = true;
            DialogueOption upperBox = optionBoxes[0];
            upperBox.SetPosition(isRight, horizontalOffset);
            upperBox.Select();
            upperBox.SetText(interactions[0]);

            int numberOfOptions = interactions.Count;
            if (numberOfOptions > 1)
            {
                DialogueOption middleBox = optionBoxes[1];
                middleBox.SetPosition(isRight, horizontalOffset);
                middleBox.Unselect();
                middleBox.SetText(interactions[1]);

                if (numberOfOptions > 2)
                {
                    DialogueOption lowerBox = optionBoxes[2];
                    lowerBox.SetPosition(isRight, horizontalOffset);
                    lowerBox.Unselect();
                    lowerBox.SetText(interactions[2]);
                }
            }
        }

        private void Start()
        {
            PlayerInputActions playerInputActions = InputManager.Instance.GetPlayerInputActions();
            playerInputActions.Interaction.Continue.performed += SelectDialogue;
            playerInputActions.Interaction.NavigateUp.performed += HighlightOptionAbove;
            playerInputActions.Interaction.NavigateDown.performed += HighlightOptionBelow;
            playerInputActions.Interaction.QAM.performed += QAM;
            playerInputActions.Exploration.QAM.performed += QAM;

            interactions.Add("Camille");
            interactions.Add("Antenna");

            gameObject.SetActive(false);
        }

        private void QAM(InputAction.CallbackContext obj)
        {
            SetupDialogueOptions(false, 100);
            gameObject.transform.position = playerTransform.position;
            gameObject.SetActive(true);

            if (GameManager.Instance.gameState == GameState.EXPLORATION)
            {
                GameManager.Instance.SetGameState(GameState.INTERACTION);
                isSelecting = true;
                gameObject.SetActive(true);
            }
            else if (GameManager.Instance.gameState == GameState.INTERACTION)
            {
                GameManager.Instance.SetGameState(GameState.EXPLORATION);
                isSelecting = false;
                gameObject.SetActive(false);
            }


        }

        private void SelectDialogue(InputAction.CallbackContext obj)
        {
            if (!isSelecting) return;
            
            optionBoxes[0].ResetOption();
            optionBoxes[1].ResetOption();
            optionBoxes[2].ResetOption();
            gameObject.SetActive(false);
            isSelecting = false;

        }

        private void HighlightOptionAbove(InputAction.CallbackContext obj)
        {
            if (!isSelecting) return;
            if (currentOptionIndex == 0) return;

            int currentIndex = currentOptionIndex;
            currentOptionIndex--;
            if (currentIndex == interactions.Count - 1 || currentIndex == 1)
                ChangeDialogueBoxes(true);
            else
                ChangeDialogueText();
        }

        private void HighlightOptionBelow(InputAction.CallbackContext obj)
        {
            if (!isSelecting) return;
            if (currentOptionIndex == interactions.Count - 1) return;

            int currentIndex = currentOptionIndex;
            currentOptionIndex++;
            if (currentIndex == interactions.Count - 2 || currentIndex == 0)
                ChangeDialogueBoxes(false);
            else
                ChangeDialogueText();
        }

        private void ChangeDialogueBoxes(bool isAbove)
        {
            if (currentOptionIndex == 0)
            {
                optionBoxes[0].Select();
                optionBoxes[1].Unselect();
            }
            else if (currentOptionIndex == interactions.Count - 1 && currentOptionIndex != 1)
            {
                optionBoxes[2].Select();
                optionBoxes[1].Unselect();
            }
            else
            {
                optionBoxes[1].Select();
                if (isAbove)
                    optionBoxes[2].Unselect();
                else
                    optionBoxes[0].Unselect();
            }
        }

        private void ChangeDialogueText()
        {
            optionBoxes[0].SetText(interactions[currentOptionIndex - 1]);
            optionBoxes[1].SetText(interactions[currentOptionIndex]);
            optionBoxes[2].SetText(interactions[currentOptionIndex + 1]);
        }
    }
}