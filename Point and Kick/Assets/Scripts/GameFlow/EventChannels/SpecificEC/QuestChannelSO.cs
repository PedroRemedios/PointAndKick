using System.Collections.Generic;
using GameFlow.Questing;
using UnityEngine;

namespace GameFlow.EventChannels.SpecificEC {
    [CreateAssetMenu(fileName = "QuestChannel", menuName = "Events/Quest Channel", order = 3)]
    public class QuestChannelSO : ScriptableObject {
        public delegate void QuestCallback(QuestSO quest);
        public event QuestCallback OnQuestStateChanged;
        
        public delegate void QuestTaskCallback(QuestSO quest, TaskSO task);
        public event QuestTaskCallback OnTaskStateChanged;
        
        public delegate List<QuestSO> QuestStateCallback(QuestState state);
        public event QuestStateCallback OnQuestInStateRequested;

        public void ChangeQuestState(QuestSO quest) {
            OnQuestStateChanged?.Invoke(quest);
        }
        
        public void ChangeTaskState(QuestSO quest, TaskSO task) {
            OnTaskStateChanged?.Invoke(quest, task);
        }

        public List<QuestSO> RequestQuestsInState(QuestState state) {
            return OnQuestInStateRequested?.Invoke(state);
        }
    }
}