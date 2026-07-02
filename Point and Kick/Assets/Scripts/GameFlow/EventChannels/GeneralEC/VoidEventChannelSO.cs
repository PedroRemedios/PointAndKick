using UnityEngine;

namespace GameFlow.EventChannels.GeneralEC {
    [CreateAssetMenu(fileName = "VoidEventChannel", menuName = "Events/Void Event Channel", order = 0)]
    public class VoidEventChannelSO : ScriptableObject {
        public delegate void VoidEvent();
        public event VoidEvent OnVoidEventRaised;

        public void RaiseEvent() {
            OnVoidEventRaised?.Invoke();
        }
    }
}