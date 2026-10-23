using System;
using UnityEngine;

namespace UI.HealthStatus {
    [CreateAssetMenu(fileName = "HitParticlesUI", menuName = "Scriptable Objects/UI/Hit Particles UI", order = 2)]
    public class HitParticlesUISO : ScriptableObject {
        [Header("Green Hit Particle")]
        public GameObject greenParticle;
        public float greenLowerTrigger;
        
        [Header("Yellow Hit Particle")]
        public GameObject yellowParticle;
        public float yellowLowerTrigger;

        [Header("Orange Hit Particle")]
        public GameObject orangeParticle;
        public float orangeLowerTrigger;
        
        [Header("Red Hit Particle")]
        public GameObject redParticle;
        public float redLowerTrigger;
        
        [Header("Broken Hit Particle")]
        public GameObject brokenParticle;
    }
}