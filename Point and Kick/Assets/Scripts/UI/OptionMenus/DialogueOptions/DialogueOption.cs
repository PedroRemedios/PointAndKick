using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.OptionMenus.DialogueOptions {
    public class DialogueOption : MonoBehaviour {
        [SerializeField] private Sprite spriteSelected;
        [SerializeField] private Sprite spriteNotSelected;

        private RectTransform rectTransform;
        private Image optionImage;
        private TextMeshProUGUI optionText;

        private void Awake() {
            rectTransform = gameObject.GetComponent<RectTransform>();
            optionImage = gameObject.GetComponent<Image>();
            optionText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            
            gameObject.SetActive(false);
        }

        public void ResetOption() {
            gameObject.SetActive(false);
        }

        public void SetPosition(bool isRight, float horizontalOffset) {
            var position = rectTransform.localPosition;
            if (isRight) {
                rectTransform.localPosition = new Vector3(horizontalOffset, position.y, position.z);
                rectTransform.pivot = new Vector2(0, rectTransform.pivot.y);
            }
            else {
                rectTransform.localPosition = new Vector3(-horizontalOffset, position.y, position.z);
                rectTransform.pivot = new Vector2(1, rectTransform.pivot.y);
            }
            gameObject.SetActive(true);
        }

        public void SetText(string text) {
            optionText.text = text;
        }

        public void Select() {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 120);
            optionImage.sprite = spriteSelected;
            optionText.fontSize = 48f;
        }

        public void Unselect() {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 225);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 90);
            optionImage.sprite = spriteNotSelected;
            optionText.fontSize = 36f;
        }
    }
}