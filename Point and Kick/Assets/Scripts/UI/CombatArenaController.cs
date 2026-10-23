using System;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;

namespace UI {
    public class CombatArenaController : MonoBehaviour {
        [SerializeField] private CombatChannel _combatChannel;

        private Animator _animator;

        private void Awake() {
            _animator = GetComponent<Animator>();

            _combatChannel.OnCombatStarted += StartCombat;
        }

        private void OnDestroy() {
            _combatChannel.OnCombatStarted -= StartCombat;
        }

        private void Start() {
            gameObject.SetActive(false);
        }

        private void StartCombat() {
            gameObject.SetActive(true);
            _animator.SetTrigger("combatStart");
        }

        public void FinishAnimation() {
            _combatChannel.ToggleBorderAnimation();
            gameObject.SetActive(false);
        }
    }
}