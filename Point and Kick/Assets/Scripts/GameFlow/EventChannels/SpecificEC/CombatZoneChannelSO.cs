using UnityEngine;

namespace GameFlow.EventChannels.SpecificEC {
    [CreateAssetMenu(fileName = "CombatZoneChannel", menuName = "Events/Combat Zone Channel", order = 0)]
    public class CombatZoneChannelSO : ScriptableObject {
        public delegate bool GameObjectCallback(GameObject value);
        public event GameObjectCallback OnZoneOccupiedByRequested;
        
        public delegate bool GeneralCallback();
        public event GeneralCallback OnZoneOccupiedRequested;

        public bool IsZoneOccupiedBy(GameObject value) {
            return OnZoneOccupiedByRequested != null && OnZoneOccupiedByRequested(value);
        }

        public bool IsZoneOccupied() {
            return OnZoneOccupiedRequested != null && OnZoneOccupiedRequested();
        }
    }
}