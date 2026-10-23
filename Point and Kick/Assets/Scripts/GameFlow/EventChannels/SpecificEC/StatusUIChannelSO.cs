using UI.HealthStatus;
using UnityEngine;

namespace GameFlow.EventChannels.SpecificEC {
    [CreateAssetMenu(fileName = "StatusUIChannel", menuName = "Events/Status UI Channel", order = 6)]
    public class StatusUIChannelSO : ScriptableObject {
        public delegate void UpdateStatusCallback(float value1, float value2);
        public event UpdateStatusCallback OnStatusHealthUpdated;
        public event UpdateStatusCallback OnStatusShieldUpdated;

        public delegate void InitializeStatusCallback(StatusUISO statusUI);
        public event InitializeStatusCallback OnStatusInitialized;

        public delegate void EndStatusCallback();
        public event EndStatusCallback OnStatusEnd;

        public void UpdateStatusHealth(float currentHealth, float baseHealth) {
            OnStatusHealthUpdated?.Invoke(currentHealth, baseHealth);
        }
        
        public void UpdateStatusShield(float currentShield, float baseShield) {
            OnStatusShieldUpdated?.Invoke(currentShield, baseShield);
        }

        public void InitializeStatus(StatusUISO statusUI) {
            OnStatusInitialized?.Invoke(statusUI);
        }

        public void EndStatus() {
            OnStatusEnd?.Invoke();
        }
    }
}