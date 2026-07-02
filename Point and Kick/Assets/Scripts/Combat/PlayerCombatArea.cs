using System.Collections.Generic;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;

namespace Combat {
    public class PlayerCombatArea : MonoBehaviour {
        [SerializeField] private CombatZoneChannelSO zoneOccupationChannel;
        [SerializeField] private int enemyLimit;
        
        public List<GameObject> objectsInArea;

        private void Awake() {
            objectsInArea = new List<GameObject>();
            
            zoneOccupationChannel.OnZoneOccupiedByRequested += IsEnemyInArea;
            zoneOccupationChannel.OnZoneOccupiedRequested += IsAreaFull;
        }

        private void OnDisable() {
            zoneOccupationChannel.OnZoneOccupiedByRequested -= IsEnemyInArea;
            zoneOccupationChannel.OnZoneOccupiedRequested -= IsAreaFull;
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;
            
            objectsInArea.Add(col.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (!objectsInArea.Contains(other.gameObject)) return;
            
            objectsInArea.Remove(other.gameObject);
        }

        private bool IsAreaFull() {
            return objectsInArea.Count >= enemyLimit;
        }
        
        private bool IsEnemyInArea(GameObject value) {
            return objectsInArea.Contains(value);
        }
    }
}