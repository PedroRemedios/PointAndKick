using UnityEngine;

namespace UI.HealthStatus {
    [CreateAssetMenu(fileName = "HitParticlesForces", menuName = "Scriptable Objects/UI/Hit Particles Forces", order = 3)]
    public class HitParticlesForcesSO : ScriptableObject {
        [Header("Movement Forces")]
        public Vector2 moveForces;

        [Header("Rotation Force")]
        public float rotationForce;
    }
}