using System;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;

namespace Entities.Enemies.Pathfinding {
    public class EnemyFollowAI : EnemyAI {
        [NonSerialized] public bool isFollowingRight;
        
        [Header("Player Combat Zones")]
        [SerializeField] private CombatZoneChannelSO playerCombatAreaChannel;
        [SerializeField] private CombatZoneChannelSO leftZoneChannel;
        [SerializeField] private CombatZoneChannelSO rightZoneChannel;

        private readonly Vector2 sideDistance = new Vector2(1.6f, 0);

        protected override Vector2 GetDestinationPosition() {
            if (!PlayerInVision() || (playerCombatAreaChannel.IsZoneOccupied() && !playerCombatAreaChannel.IsZoneOccupiedBy(gameObject)))
                return Vector2.zero;

            Vector2 enemyPosition = transform.position;
            Vector2 playerPosition = targetPosition.position;
            
            float leftDistance = Vector2.Distance(enemyPosition, playerPosition - sideDistance);
            float rightDistance = Vector2.Distance(enemyPosition, playerPosition + sideDistance);

            if (leftDistance < rightDistance) {
                if (!leftZoneChannel.IsZoneOccupied())
                    return playerPosition - sideDistance;
            
                if (leftZoneChannel.IsZoneOccupiedBy(gameObject))
                    return playerPosition - sideDistance;

                if (!rightZoneChannel.IsZoneOccupied())
                    return playerPosition + sideDistance;

                if (rightZoneChannel.IsZoneOccupiedBy(gameObject)) {
                    return playerPosition + sideDistance;
                }

                return Vector2.zero;
            }
            else {
                if (!rightZoneChannel.IsZoneOccupied())
                    return playerPosition + sideDistance;
            
                if (rightZoneChannel.IsZoneOccupiedBy(gameObject))
                    return playerPosition + sideDistance;

                if (!leftZoneChannel.IsZoneOccupied())
                    return playerPosition - sideDistance;

                if (leftZoneChannel.IsZoneOccupiedBy(gameObject))
                    return playerPosition - sideDistance;
            
                return Vector2.zero;
            }
        }
    }
}