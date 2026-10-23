using UnityEngine;

namespace Entities.Player.Movement {
    [CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Scriptable Objects/Player/Movement Data", order = 0)]
    public class PlayerMovementDataSO : ScriptableObject {
        [Header("Moving Parameters")]
        public float walkSpeed;
        public float runSpeed;
    
        [Header("Jumping Parameters")]
        public float jumpPower;
        public float characterWeight;

        [Header("Scaling")]
        public float scalePerUnit;
    }
}