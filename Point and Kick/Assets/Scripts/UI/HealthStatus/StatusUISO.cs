using UnityEngine;

namespace UI.HealthStatus {
    [CreateAssetMenu(fileName = "StatusUI", menuName = "Scriptable Objects/UI/Status UI", order = 1)]
    public class StatusUISO : ScriptableObject {
        [Header("Player Portrait")]
        public Sprite playerPortrait;
        
        [Header("HP Status")]
        public Sprite hpSprite;
        public Sprite hpBackgroundSprite;
        public Sprite hpCoreSprite;
        
        [Header("Shield Status")]
        public Sprite shieldSprite;
        
        [Header("Fill Parameters")]
        public float maxFillAmount;
        public float minFillAmount;
    }
}