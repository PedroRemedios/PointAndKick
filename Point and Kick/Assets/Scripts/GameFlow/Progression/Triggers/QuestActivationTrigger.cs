using System;
using GameFlow.EventChannels.SpecificEC;
using GameFlow.Questing;
using UnityEngine;

namespace GameFlow.Progression.Triggers {
    public class QuestActivationTrigger : MonoBehaviour {
        [SerializeField] private QuestChannelSO questChannel;
        [SerializeField] private QuestContainerSO questContainer;

        private void Start() {
            if (questContainer == null) return;
            foreach (var quest in questContainer.quests) {
                quest.MakeQuestAvailable();
            }
        }
    }
}