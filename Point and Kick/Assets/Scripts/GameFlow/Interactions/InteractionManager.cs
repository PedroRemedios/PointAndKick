using System;
using System.Collections.Generic;
using GameFlow.EventChannels.SpecificEC;
using GameFlow.Interactions.InteractionVariants;
using GameFlow.Progression.Conditions;
using GameFlow.Questing;
using UI.OptionMenus.DialogueOptions;
using UnityEngine;

namespace GameFlow.Interactions {
    public class InteractionManager : MonoBehaviour, InteractionVisitor
    {
        [SerializeField] private InteractionManagerSO interactionData;
        [SerializeField] private NarrationSpeaker actor;
        
        public InteractionChannelSO interactionChannel = default;
        public DialogueOptionsChannelSO optionsChannel;
        public QuestChannelSO questChannel;

        private List<LookAtInteractionSO> lookAts = new List<LookAtInteractionSO>();
        private List<DialogueInteractionSO> dialogues = new List<DialogueInteractionSO>();
        private Dictionary<CompositeDialogueInteractionSO, int> compositeDialogues = new Dictionary<CompositeDialogueInteractionSO, int>();
        
        private LookAtInteractionSO currentLookAt; 
        private List<OptionInterface> interactionOptions = new List<OptionInterface>();

        public InteractionPopupSO interactionPopup;

        private void Awake() {
            if (interactionData.defaultDialogue != null)
                interactionOptions.Add(new InteractionOption(interactionData.defaultDialogue));
            
            if (interactionData.exitDialogue != null)
                interactionOptions.Add(new InteractionOption(interactionData.exitDialogue));
            
            foreach (InteractionSO interaction in interactionData.interactions) {
                interaction.AddToManager(this);
            }
            
            if (lookAts.Count > 0)
                currentLookAt = lookAts[0];
        }

        private void OnEnable() {
            optionsChannel.OnOptionSelected += BeginDialogue;
            interactionChannel.OnInteractionEnd += InteractionEnd;
            questChannel.OnTaskStateChanged += TaskStateChanged;
            
            foreach (InteractionSO interaction in interactionData.interactions) {
                interaction.OnInteractionEnabled += EnableInteraction;
                interaction.OnInteractionDisabled += DisableInteraction;
            }
        }

        private void OnDisable() {
            optionsChannel.OnOptionSelected -= BeginDialogue;
            interactionChannel.OnInteractionEnd -= InteractionEnd;
            questChannel.OnTaskStateChanged -= TaskStateChanged;
            
            foreach (InteractionSO interaction in interactionData.interactions) {
                interaction.OnInteractionEnabled -= EnableInteraction;
                interaction.OnInteractionDisabled -= DisableInteraction;
            }
        }
        
        //
        // QUEST BASED CHOICES AND PROGRESSION
        //

        private void EnableInteraction(InteractionSO interaction) {
            interaction.EnableInteraction(this);
        }

        private void DisableInteraction(InteractionSO interaction) {
            foreach (var option in interactionOptions) {
                if (interaction != option.GetInteraction()) continue;
                interactionOptions.Remove(option);
                break;
            }
        }
        
        private void TaskStateChanged(QuestSO quest, TaskSO task) {
            EnableTaskOptions(task);
            DisableTaskOptions(task);
        }

        private void EnableTaskOptions(TaskSO task) {
            if (task.actor == actor && task.taskState == TaskState.Ongoing)
                interactionOptions.Insert(interactionOptions.Count - 1, new TaskOption(task));
        }

        private void DisableTaskOptions(TaskSO task) {
            if (task.actor != actor || task.taskState != TaskState.Completed) return;
            
            foreach (var option in interactionOptions) {
                if (option.GetTask() != task) continue;
                interactionOptions.Remove(option);
                break;
            }
        }
        
        //
        // INTERACTION ACTIONS 
        //

        public void ListDialogues(bool isRight) {
            GameManager.Instance.SetGameState(GameState.INTERACTION);
            
            DialogueOptionsArgs dialogueOptions = new DialogueOptionsArgs(interactionOptions, transform.position, actor, isRight);
            optionsChannel.ProvideOptions(dialogueOptions);
        }

        private void BeginDialogue(InteractionSO interaction, NarrationSpeaker optionsActor) {
            if (optionsActor != actor) return;
            DialogueInteractionSO dialogue = interaction.GetDialogueFromInteraction(this);
            interactionChannel.RequestInteraction(dialogue);
        }

        public void BeginLookAt() {
            GameManager.Instance.SetGameState(GameState.INTERACTION);
            interactionChannel.RequestInteraction(currentLookAt);
        }
        
        private void InteractionEnd(InteractionSO interaction) {
            GameManager.Instance.SetGameState(GameState.EXPLORATION);
            if (interaction.actor != actor) return;
            interaction.Accept(this);
        }
        
        //
        // VISITORS
        //
        
        // INTERACTION ENABLER BEHAVIOUR
        
        public void EnableInteraction(LookAtInteractionSO lookAt) {
            currentLookAt = lookAt;
        }
        
        public void EnableInteraction(DialogueInteractionSO dialogue) {
            interactionOptions.Insert(interactionOptions.Count - 1, new InteractionOption(dialogue));
        }
        
        public void EnableInteraction(CompositeDialogueInteractionSO dialogue) {
            interactionOptions.Insert(interactionOptions.Count - 1, new InteractionOption(dialogue));
        }
        
        // MANAGER DATA CONTAINERS INITIALIZATION
        
        public void Add(LookAtInteractionSO interaction) {
            lookAts.Add(interaction);
        }

        public void Add(DialogueInteractionSO interaction) {
            dialogues.Add(interaction);
        }

        public void Add(CompositeDialogueInteractionSO interaction) {
            compositeDialogues.Add(interaction, 0);
        }
        
        // LOOKAT PROGRESSION WITH DIALOGUE TRIGGERS
        
        public void Visit(LookAtInteractionSO interaction) { }

        public void Visit(DialogueInteractionSO interaction) {
            if (currentLookAt == null) return;
            foreach (InteractionTrigger trigger in currentLookAt.interactionTriggers) {
                if (trigger.triggerInteraction != null && trigger.triggerInteraction != interaction) continue;
                currentLookAt = trigger.nextLookAt;
                break;
            }
        }
        
        // DIALOGUE SELECTION FROM COMPOSITE

        public DialogueInteractionSO ChooseDialogueFromComposite(CompositeDialogueInteractionSO compositeDialogue) {
            DialogueInteractionSO dialogue = compositeDialogue.dialogueSequence[compositeDialogues[compositeDialogue]];
            if (compositeDialogues[compositeDialogue] + 1 < compositeDialogue.dialogueSequence.Length) {
                compositeDialogues[compositeDialogue]++;
            }
            return dialogue;
        }
    }
}
