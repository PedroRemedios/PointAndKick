using System;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using UnityEngine;

namespace GameFlow.Questing {
    [CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Questing/Quest", order = 1)]
    public class QuestSO : ScriptableObject {
        public delegate void QuestStateChanged(QuestSO quest);
        public event QuestStateChanged OnQuestStateChanged;
        
        public delegate void QuestTaskStateChanged(QuestSO quest, TaskSO task);
        public event QuestTaskStateChanged OnQuestTaskStateChanged;
        
        [Header("Ordered Tasks")]
        public TaskSO[] tasks;
        
        [Header("Quest State (READ ONLY)")]
        public QuestState state;
        public QuestState questState {
            get => state;
            private set {
                var before = state;
                state = value;

                if (before != state) {
                    OnQuestStateChanged?.Invoke(this);
                }
            }
        }

        private void OnEnable() {
            foreach (var task in tasks) {
                task.OnTaskStateChanged += TaskStateChanged;
            }
        }

        private void OnDisable() {
            foreach (var task in tasks) {
                task.OnTaskStateChanged -= TaskStateChanged;
            }
        }

        private void TaskStateChanged(TaskSO task) {
            if (task.taskState == TaskState.Completed) {
                if (questState == QuestState.Available)
                    ActivateQuest();
                
                var nextTask = tasks.FirstOrDefault(varTask => varTask.taskState == TaskState.Unavailable);
                if (nextTask != null)
                    nextTask.Activate();
                else
                    CompleteQuest();
            }
            
            OnQuestTaskStateChanged?.Invoke(this, task);
        }

        private void CompleteQuest() {
            if (questState != QuestState.Ongoing) return;
            questState = QuestState.Completed;
        }

        public void MakeQuestAvailable() {
            if (questState != QuestState.Unavailable) return;
            questState = QuestState.Available;
            tasks[0].Activate();
        }

        private void ActivateQuest() {
            if (questState != QuestState.Available) return;
            questState = QuestState.Ongoing;
        }

        public void ResetProgression() {
            questState = QuestState.Unavailable;
            foreach (var task in tasks) {
                task.ResetProgression();
            }
        }

        public void UpdateItemProgression(ItemStack itemStack) {
            foreach (var task in tasks) {
                task.UpdateItemProgression(itemStack);
            }
        }
    }
}
