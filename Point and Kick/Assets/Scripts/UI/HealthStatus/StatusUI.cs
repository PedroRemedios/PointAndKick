using System;
using GameFlow.EventChannels.SpecificEC;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HealthStatus {
    public class StatusUI : MonoBehaviour {
        [Header("Listening On")]
        [SerializeField] private StatusUIChannelSO statusUIChannel;
        
        [Header("Status UI Elements")]
        [SerializeField] private Image playerPortrait;
        [SerializeField] private Image hpMeterBackground;
        [SerializeField] private Image hpMeter;
        [SerializeField] private Image hpCore;

        private float maxHealthUI;
        private float minHealthUI;

        private void OnEnable() {
            statusUIChannel.OnStatusInitialized += InitializeStatusUI;
            statusUIChannel.OnStatusEnd += EndStatusUI;
            statusUIChannel.OnStatusHealthUpdated += UpdatePlayerHealthUI;
        }

        private void OnDisable() {
            statusUIChannel.OnStatusInitialized -= InitializeStatusUI;
            statusUIChannel.OnStatusEnd -= EndStatusUI;
            statusUIChannel.OnStatusHealthUpdated -= UpdatePlayerHealthUI;
        }

        private void InitializeStatusUI(StatusUISO statusUI) {
            if (hpCore != null) {
                hpCore.gameObject.SetActive(true);
                hpCore.sprite = statusUI.hpCoreSprite;
            }

            if (playerPortrait != null) {
                playerPortrait.gameObject.SetActive(true);
                playerPortrait.sprite = statusUI.playerPortrait;
            }

            if (hpMeterBackground != null) {
                hpMeterBackground.gameObject.SetActive(true);
                hpMeterBackground.sprite = statusUI.hpBackgroundSprite;
            }

            if (hpMeter != null) {
                hpMeter.gameObject.SetActive(true);
                hpMeter.sprite = statusUI.hpSprite;
            }
            
            maxHealthUI = statusUI.maxFillAmount;
            minHealthUI = statusUI.minFillAmount;
        }

        private void EndStatusUI() {
            if (hpCore != null)
                hpCore.gameObject.SetActive(false);
            
            if (playerPortrait != null)
                playerPortrait.gameObject.SetActive(false);
            
            if (hpMeterBackground != null)
                hpMeterBackground.gameObject.SetActive(false);
            
            if (hpMeter != null)
                hpMeter.gameObject.SetActive(false);
        }

        private void UpdatePlayerHealthUI(float currentHealth, float baseHealth) {
            if (currentHealth <= 0)
                hpMeter.fillAmount = 0;
            else {
                float healthPercentage = currentHealth / baseHealth;
                float fillAmount = minHealthUI + (maxHealthUI - minHealthUI) * healthPercentage;
                hpMeter.fillAmount = fillAmount;
            }
        }
    }
}