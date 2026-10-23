using System;
using System.Collections;
using UnityEngine;

namespace UI.HealthStatus {
    [RequireComponent(typeof(Rigidbody2D))]
    public class HitParticlesUI : MonoBehaviour {
        public HitParticlesForcesSO hitParticlesForces;
        private new Rigidbody2D rigidbody2D;
        
        private void Awake() {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Move(float particleDirection) {
            rigidbody2D.AddForce(new Vector2(particleDirection * hitParticlesForces.moveForces.x, hitParticlesForces.moveForces.y), ForceMode2D.Impulse);
            rigidbody2D.AddTorque(particleDirection * hitParticlesForces.rotationForce, ForceMode2D.Impulse);
            StartCoroutine(FinishHitParticle());
        }

        private IEnumerator FinishHitParticle() {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }
}