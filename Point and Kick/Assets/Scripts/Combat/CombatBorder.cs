using System;
using Entities.Enemies;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;

namespace Combat {
    public class CombatBorder : MonoBehaviour {
        private enum BorderSide { Left, Right }

        [SerializeField] private BorderSide _borderSide;
        [SerializeField] private CombatChannel _combatChannel;

        private Animator _animator;

        private void Awake() {
            _animator = GetComponent<Animator>();

            _combatChannel.OnBorderAnimation += ToggleBorderAnimation;
        }

        private void OnDestroy() {
            _combatChannel.OnBorderAnimation -= ToggleBorderAnimation;
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (!other.CompareTag("Enemy")) return;
            
            EnemyController controller = other.GetComponent<EnemyController>();
            controller.startedCombat = true;
            other.isTrigger = false;
        }

        private void ToggleBorderAnimation() {
            switch (_borderSide) {
                case BorderSide.Left:
                    _animator.SetBool("leftBorder", !_animator.GetBool("leftBorder"));
                    break;
                case BorderSide.Right:
                    _animator.SetBool("rightBorder", !_animator.GetBool("rightBorder"));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}