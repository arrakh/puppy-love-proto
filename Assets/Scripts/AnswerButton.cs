using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class AnswerButton : MonoBehaviour
    {
        public float startSpeedBoost = 4000f;
        public float startSpeedBoostVariance = 300f;
        public float targetSpeed = 200f;
        public float targetSpeedVariance = 100f;
        public float deceleration = 6000f;
        public float decelerationVariance = 400f;
        public bool isStatic = false;
        
        public float smoothTime = 0.1f;
        
        public Button button;
        public TextMeshProUGUI answerText;
        public RectTransform rect;
        public RectTransform textRect;
        public Image background;

        private bool hasInit = false;
        private float currentSpeed;
        private float currentVelocity;
        private Vector2 direction;
        private RectTransform boundariesRect;

        public void CopyVisual(AnswerButton btn)
            => Set(btn.boundariesRect, btn.background.color, btn.answerText.color, btn.answerText.text, null);

        public void Set(RectTransform boundaries, Color color, Color textColor, string answer, Action<string> onAnswerButton)
        {
            boundariesRect = boundaries;
            hasInit = false;
            background.color = color;
            answerText.color = textColor;
            answerText.text = answer;

            direction = Random.insideUnitCircle.normalized;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { onAnswerButton?.Invoke(answer); });
            
            currentSpeed = targetSpeed + startSpeedBoost;
            startSpeedBoost += Random.Range(-startSpeedBoostVariance, startSpeedBoostVariance);
            targetSpeed += Random.Range(-targetSpeedVariance, targetSpeedVariance);
            deceleration += Random.Range(-decelerationVariance, decelerationVariance);
        }

        private void Update()
        {
            rect.sizeDelta = new Vector2(textRect.rect.width, textRect.rect.height);

            if (isStatic) return;
            
            if (Mathf.Abs(currentSpeed - targetSpeed) > 0.01f)
            {
                currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocity, smoothTime);
                //currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, deceleration * Time.deltaTime);
            }
            
            Vector2 movement = direction * (currentSpeed * Time.deltaTime);
            rect.anchoredPosition += movement;
            
            Vector2 minBounds = boundariesRect.rect.min;
            Vector2 maxBounds = boundariesRect.rect.max;
            Vector2 buttonMin = rect.anchoredPosition - rect.sizeDelta / 2;
            Vector2 buttonMax = rect.anchoredPosition + rect.sizeDelta / 2;
            
            if (buttonMin.x <= minBounds.x || buttonMax.x >= maxBounds.x)
            {
                direction.x *= -1;
                rect.anchoredPosition = new Vector2(
                    Mathf.Clamp(rect.anchoredPosition.x, minBounds.x + rect.sizeDelta.x / 2, maxBounds.x - rect.sizeDelta.x / 2),
                    rect.anchoredPosition.y);
            }
            
            if (buttonMin.y <= minBounds.y || buttonMax.y >= maxBounds.y)
            {
                direction.y *= -1;
                rect.anchoredPosition = new Vector2(
                    rect.anchoredPosition.x,
                    Mathf.Clamp(rect.anchoredPosition.y, minBounds.y + rect.sizeDelta.y / 2, maxBounds.y - rect.sizeDelta.y / 2));
            }
        }
    }
}