using System;
using System.Collections.Generic;
using GameFlow.EventChannels.SpecificEC;
using Inventory;
using UnityEngine;

namespace GameFlow.Questing {
    public class QuestManager : MonoBehaviour {
        [SerializeField] private QuestChannelSO questChannel;
        [SerializeField] private QuestContainerSO questContainer;
        [SerializeField] private InventoryChannelSO inventoryChannel;

        private List<QuestSO> availableQuests = new List<QuestSO>();
        private List<QuestSO> ongoingQuests = new List<QuestSO>();
        private List<QuestSO> completedQuests = new List<QuestSO>();

        private QuestSO[] quests;

        private void Awake() {
            inventoryChannel.OnInventoryModified += UpdateItemProgression;
            questChannel.OnQuestInStateRequested += GetQuestsInState;
            
            quests = questContainer.quests;
            foreach (var quest in quests) {
                quest.OnQuestStateChanged += QuestStateChanged;
                quest.OnQuestTaskStateChanged += TaskStateChanged;
            }
            
            ResetProgression();
        }

        private void OnDestroy() {
            inventoryChannel.OnInventoryModified -= UpdateItemProgression;
            questChannel.OnQuestInStateRequested -= GetQuestsInState;
            foreach (var quest in quests) {
                quest.OnQuestStateChanged -= QuestStateChanged;
                quest.OnQuestTaskStateChanged -= TaskStateChanged;
            }
        }

        private List<QuestSO> GetQuestsInState(QuestState state) {
            switch (state) {
                case QuestState.Available:
                    return availableQuests;
                case QuestState.Ongoing:
                    return ongoingQuests;
                case QuestState.Completed:
                    return completedQuests;
                case QuestState.Unavailable:
                    return new List<QuestSO>();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void QuestStateChanged(QuestSO quest) {
            switch (quest.questState) {
                case QuestState.Available:
                    availableQuests.Add(quest);
                    break;
                case QuestState.Ongoing:
                    availableQuests.Remove(quest);
                    ongoingQuests.Add(quest);
                    break;
                case QuestState.Completed:
                    ongoingQuests.Remove(quest);
                    completedQuests.Add(quest);
                    break;
                case QuestState.Unavailable:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (questChannel != null)
                questChannel.ChangeQuestState(quest);
        }

        private void UpdateItemProgression(ItemStack itemStack) {
            foreach (var quest in ongoingQuests) {
                quest.UpdateItemProgression(itemStack);
            }
        }

        private void TaskStateChanged(QuestSO quest, TaskSO task) {
            if (questChannel != null)
                questChannel.ChangeTaskState(quest, task);
        }

        public void ResetProgression() {
            foreach (var quest in quests) {
                quest.ResetProgression();
            }
        }

    }
}