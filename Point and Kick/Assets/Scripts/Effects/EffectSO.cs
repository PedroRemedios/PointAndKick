using UnityEngine;

namespace Effects {
    [CreateAssetMenu(fileName = "Effect", menuName = "Scriptable Objects/Effects/Effect", order = 0)]
    public class EffectSO : ScriptableObject {
        public GameObject effectParticle;
        public Vector2 effectOffset;
    }
}