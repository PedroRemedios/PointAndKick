using GameFlow.EventChannels.GeneralEC;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;

namespace Combat {
    public class PlayerCombatZone : MonoBehaviour {
        [SerializeField] private CombatZoneChannelSO zoneOccupationChannel;
        [SerializeField] private BoolEventChannelSO playerOrientationChannel;
        
        private GameObject objectInZone;
        private BoxCollider2D boxCollider2D;

        private void Awake() {
            objectInZone = null;
            boxCollider2D = GetComponent<BoxCollider2D>();
            
            zoneOccupationChannel.OnZoneOccupiedByRequested += IsZoneOccupiedBy;
            zoneOccupationChannel.OnZoneOccupiedRequested += IsZoneOccupied;
            playerOrientationChannel.OnBoolEventRaised += PlayerOrientationChanged;
        }

        private void OnDisable() {
            zoneOccupationChannel.OnZoneOccupiedByRequested -= IsZoneOccupiedBy;
            zoneOccupationChannel.OnZoneOccupiedRequested -= IsZoneOccupied;
            playerOrientationChannel.OnBoolEventRaised -= PlayerOrientationChanged;
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (objectInZone != null || (col.gameObject.layer != LayerMask.NameToLayer("Enemy") && col.gameObject.layer != LayerMask.NameToLayer("Environment"))) return;
            
            objectInZone = col.gameObject;
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (objectInZone != other.gameObject) return;
            
            objectInZone = null;
        }

        private bool IsZoneOccupied() {
            return objectInZone != null;
        }
        
        private bool IsZoneOccupiedBy(GameObject value) {
            return objectInZone == value;
        }
        
        private void PlayerOrientationChanged(bool value) {
            Vector2 offset = boxCollider2D.offset;
            offset.x *= -1;
            boxCollider2D.offset = offset;
        }
    }
}