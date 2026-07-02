using Cinemachine;
using Combat.CombatElements;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;

namespace Combat {
    public class CombatArea : MonoBehaviour {
        [SerializeField] private CombatSO combatData;
        [SerializeField] private CombatChannel combatChannel;
        [SerializeField] private CinemachineConfiner combatCameraConfiner;

        [Header("Combat Bounds")]
        [SerializeField] private BoxCollider2D leftBorder;
        [SerializeField] private BoxCollider2D rightBorder;
        
        [Header("Spawn Locations")]
        [SerializeField] private BoxCollider2D leftSpawner;
        [SerializeField] private BoxCollider2D rightSpawner;

        private BoxCollider2D combatTrigger;
        
        private float leftSpawnerUpperBound;
        private float leftSpawnerLowerBound;
        private float rightSpawnerUpperBound;
        private float rightSpawnerLowerBound;

        private void Awake() {
            combatTrigger = GetComponent<BoxCollider2D>();
            
            leftSpawnerUpperBound = leftSpawner.offset.y + leftSpawner.size.y / 2;
            leftSpawnerLowerBound = leftSpawner.offset.y - leftSpawner.size.y / 2;
            rightSpawnerUpperBound = rightSpawner.offset.y + rightSpawner.size.y / 2;
            rightSpawnerLowerBound = rightSpawner.offset.y - rightSpawner.size.y / 2;
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.gameObject.CompareTag("Player"))
                EnterCombat();
        }

        private void EnterCombat() {
            combatChannel.OnSpawnRandomPositionRequested += GenerateRandomPosition;
            combatChannel.OnSpawnSpecificPositionRequested += GenerateSpecificPosition;
            combatChannel.SendCombatData(combatData);
            combatChannel.OnCombatEnded += EndCombat;

            PolygonCollider2D newCameraConfinerObject = transform.Find("Camera Confiner").GetComponent<PolygonCollider2D>();
            combatCameraConfiner.m_BoundingShape2D = newCameraConfinerObject;
            
            leftBorder.enabled = true;
            rightBorder.enabled = true;
            combatTrigger.enabled = false;
        }

        private void EndCombat() {
            combatChannel.OnCombatEnded -= EndCombat;
            
            leftBorder.enabled = false;
            rightBorder.enabled = false;
            combatTrigger.enabled = false;
        }

        private Vector2 GenerateRandomPosition(Vector2 position) {
            if (position.x < 0) {
                float yPosition = Random.Range(leftSpawnerLowerBound, leftSpawnerUpperBound);
                return new Vector2(transform.position.x + leftSpawner.offset.x, transform.position.y + yPosition);
            }
            else {
                float yPosition = Random.Range(rightSpawnerLowerBound, rightSpawnerUpperBound);
                return new Vector2(transform.position.x + rightSpawner.offset.x, transform.position.y + yPosition);
            }
        }

        private Vector2 GenerateSpecificPosition(Vector2 position) {
            return (Vector2) transform.position + position;
        }
    }
}