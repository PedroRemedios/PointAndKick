using System;
using System.Collections.Generic;
using GameFlow.EventChannels.SpecificEC;
using GameFlow.Progression.Conditions;
using GameFlow.Questing;
using UnityEngine;

namespace GameFlow.Progression.Triggers {
    public class QuestProgressionTrigger : MonoBehaviour {
        [SerializeField] private QuestConditionSO[] progressionConditions;
        
        [Header("Listening On")]
        [SerializeField] private QuestChannelSO questChannel;

        private List<QuestConditionSO> conditions = new List<QuestConditionSO>();

        private void Awake() {
            foreach (var condition in progressionConditions) {
                conditions.Add(condition);
            }
        }

        private void OnEnable() {
            questChannel.OnQuestStateChanged += OnQuestStateChanged;
        }

        private void OnDisable() {
            questChannel.OnQuestStateChanged -= OnQuestStateChanged;
        }

        private void OnQuestStateChanged(QuestSO quest) {
            for (var i = conditions.Count - 1; i >= 0; i--) {
                if (conditions[i].quest == quest && conditions[i].questState == quest.questState)
                    conditions.Remove(conditions[i]);
            }

            if (conditions.Count == 0) {
                TriggerAction();
            }
        }

        private void TriggerAction() {
            gameObject.SetActive(false);
            enabled = false;
        }
    }
}