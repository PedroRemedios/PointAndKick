using UnityEngine;

namespace Entities {
    [CreateAssetMenu(fileName = "ControllerData", menuName = "Scriptable Objects/Controller Data")]
    public class ControllerDataSO : ScriptableObject {
        [Header("Moving Parameters")]
        public float walkSpeed;
        public float runSpeed;
    
        [Header("Jumping Parameters")]
        public float jumpPower;
        public float characterWeight;

        [Header("Animation Parameters")] 
        public float knockedAnimationDuration;

        [Header("Scaling")]
        public float scalePerUnit;
    }
}