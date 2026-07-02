using UnityEngine;

namespace GameFlow.EventChannels.GeneralEC {
    [CreateAssetMenu(fileName = "Vector2EventChannel", menuName = "Events/Vector2EventChannel", order = 9)]
    public class Vector2EventChannelSO : ScriptableObject {
        public delegate void Vector2Event(Vector2 value);
        public event Vector2Event OnVector2EventRaised;

        public void RaiseEvent(Vector2 value) {
            OnVector2EventRaised?.Invoke(value);
        }
    }
}