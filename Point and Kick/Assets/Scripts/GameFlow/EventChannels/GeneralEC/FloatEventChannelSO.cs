using UnityEngine;

namespace GameFlow.EventChannels.GeneralEC {
    [CreateAssetMenu(fileName = "FloatEventChannel", menuName = "Events/Float Event Channel", order = 3)]
    public class FloatEventChannelSO : ScriptableObject {
        public delegate void FloatEvent(float value);
        public event FloatEvent OnFloatEventRaised;

        public void RaiseEvent(float value) {
            OnFloatEventRaised?.Invoke(value);
        }
    }
}