using UnityEngine;

namespace GameFlow.Interactions {
    [CreateAssetMenu(menuName = "Scriptable Objects/Interactions/Interaction Manager")]
    public class InteractionManagerSO : ScriptableObject {
        [Header("Default Dialogue")] 
        public InteractionSO defaultDialogue;
        
        [Header("Exit Dialogue")] 
        public InteractionSO exitDialogue;
    
        [Header ("All Interactions")]
        public InteractionSO[] interactions;
    }
}
