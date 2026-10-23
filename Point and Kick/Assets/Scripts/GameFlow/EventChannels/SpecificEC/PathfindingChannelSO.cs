using UnityEngine;

namespace GameFlow.EventChannels.SpecificEC {
    [CreateAssetMenu(fileName = "PathfindingChannel", menuName = "Events/Pathfinding Channel")]
    public class PathfindingChannelSO : ScriptableObject {
        public delegate Vector2 PathfindingPositionCallback();
        public event PathfindingPositionCallback OnPathfindingPositionRequested;

        public Vector2 RequestPathfindingPosition() {
            return OnPathfindingPositionRequested != null ? OnPathfindingPositionRequested.Invoke() : Vector2.zero;
        }
    }
}