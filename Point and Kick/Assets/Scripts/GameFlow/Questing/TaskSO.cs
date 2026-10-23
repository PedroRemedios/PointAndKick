using System;
using System.Collections.Generic;
using System.Linq;
using GameFlow.EventChannels.SpecificEC;
using Inventory;
using UnityEngine;

namespace GameFlow.Questing {
    [CreateAssetMenu(fileName = "Task", menuName = "Scriptable Objects/Questing/Task", order = 2)]
    public class TaskSO : ScriptableObject {
        public enum TaskType {
            Interaction,
            GiveItem
            // CheckItem
        }
        
        public delegate void TaskStateChanged(TaskSO task);
        public event TaskStateChanged OnTaskStateChanged;
        
        [Header("Quest Channel")]
        public QuestChannelSO questChannel;
        
        [Header("Task Actor Giver")]
        public NarrationSpeaker actor;
        
        [Header("Interactions Based on Completion")]
        public InteractionSO incompleteInteraction;
        public InteractionSO completeInteraction;
        
        [Header("Type of Task")]
        public TaskType taskType;

        [Header("Item Task: Item to Give/Check")]
        public int item;
        public int itemProgress;
        
        [Header("Task State (READ ONLY)")]
        public TaskState state;
        public TaskState taskState {
            get => state;
            private set {
                var before = state;
                state = value;

                if (before != state) {
                    OnTaskStateChanged?.Invoke(this);
                }
            }
        }
        
        [Header("Task Progress (READ ONLY)")]
        public int progress;
        public int taskProgress {
            get => progress;
            private set => progress = value;
        }
        
        

        public InteractionSO TaskInteractionSelected() {
            switch (taskState) {
                case TaskState.Ongoing:
                    return incompleteInteraction;
                case TaskState.ConditionMet:
                    // TODO: Reward Verification of sorts here
                    FinishTask();
                    return completeInteraction;
                case TaskState.Unavailable:
                    return null;
                case TaskState.Completed:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void UpdateItemProgression(ItemStack itemStack) {
            if (taskState != TaskState.Ongoing && taskState != TaskState.ConditionMet) return;
            
            if (itemStack.GetItem() == item) {
                taskProgress += itemStack.GetAmount();
                CheckCondition();
            }
        }

        private void CheckCondition() {
            if (taskProgress >= itemProgress) {
                taskProgress = itemProgress;
                taskState = TaskState.ConditionMet;
            }
            else
                taskState = TaskState.Ongoing;
        }

        public void Activate() {
            if (taskState != TaskState.Unavailable) return;
            
            taskState = TaskState.Ongoing;
            if (taskType == TaskType.Interaction)
                taskState = TaskState.ConditionMet;
        }

        public void FinishTask() {
            if (taskState != TaskState.ConditionMet) return;
            taskState = TaskState.Completed;
        }

        public void ResetProgression() {
            taskState = TaskState.Unavailable;
            taskProgress = 0;
        }
    }
}