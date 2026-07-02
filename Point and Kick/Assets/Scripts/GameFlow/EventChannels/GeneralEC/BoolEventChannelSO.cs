using UnityEngine;

namespace GameFlow.EventChannels.GeneralEC {
    [CreateAssetMenu(fileName = "BoolEventChannel", menuName = "Events/Bool Event Channel", order = 2)]
    public class BoolEventChannelSO : ScriptableObject {
        public delegate void BoolEvent(bool value);
        public event BoolEvent OnBoolEventRaised;

        public delegate bool BoolReturnEvent();
        public event BoolReturnEvent OnBoolReturnEventRaised;
        

        public void RaiseEvent(bool value) {
            OnBoolEventRaised?.Invoke(value);
        }

        public bool RaiseReturnEvent() {
            return OnBoolReturnEventRaised != null && OnBoolReturnEventRaised();
        }
    }
}