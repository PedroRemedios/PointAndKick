using UnityEngine;

namespace GameFlow.Questing {
    [CreateAssetMenu(fileName = "QuestContainer", menuName = "Scriptable Objects/Questing/Quest Container", order = 0)]
    public class QuestContainerSO : ScriptableObject {
        public QuestSO[] quests;
    }
}