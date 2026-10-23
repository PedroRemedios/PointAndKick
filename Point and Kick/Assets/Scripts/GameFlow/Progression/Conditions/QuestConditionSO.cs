using GameFlow.Questing;
using UnityEngine;

namespace GameFlow.Progression.Conditions {
    [CreateAssetMenu(fileName = "QuestCondition", menuName = "Scriptable Objects/Questing/Quest Condition", order = 3)]
    public class QuestConditionSO : ScriptableObject {
        [Header("Required Quest")] public QuestSO quest;
        [Header("Required State")] public QuestState questState;
    }
}