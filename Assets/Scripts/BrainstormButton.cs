using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class BrainstormButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action OnBrainstorm;

        public float chargeTime = 1.4f;
        public float overthinkTime = 3.5f;
        public float outputDelay = 0.2f;
        public AnimationCurve outputDelayModifier;
        public Image fillImage;
        public Image realFillImage;
        public GameObject overthinkingGroup;
        
        [Header("Animation")]
        public RectTransform scaleParent;
        public AnimationCurve scaleCurve;
        public float scaleDuration;
        public float scaleFrom;

        public RectTransform shakeParent;
        public AnimationCurve shakeCurve;
        public float shakeDistance;
        public AnimationCurve fillCurve;

        private bool isHolding = false;
        private bool isOverthinking = false;
        private float currentChargeTimer;
        private float currentOverthinkTimer;
        private float currentDelayTimer;

        private Tween scaleTween;
        
        private float ChargeAlpha => currentChargeTimer / chargeTime;
        private float OverthinkAlpha => currentOverthinkTimer / overthinkTime;

        private void Update()
        {
            EvaluateUpdate();
            VisualUpdate();
        }

        private void VisualUpdate()
        {
            overthinkingGroup.SetActive(isOverthinking);
            var alpha = isOverthinking ? 1f - OverthinkAlpha : ChargeAlpha;
            SetFillAlpha(alpha);
            SetButtonShake(alpha);
        }

        private void SetButtonShake(float alpha)
        {
            var modifier = shakeCurve.Evaluate(alpha);
            var distance = shakeDistance * modifier;
            shakeParent.localPosition = Random.insideUnitCircle * distance;
        }

        private void SetFillAlpha(float alpha)
        {
            var a = isOverthinking ? alpha : fillCurve.Evaluate(alpha);
            
            fillImage.fillAmount = a;

            var color = realFillImage.color;
            color.a = a;
            realFillImage.color = color;
        }

        private void EvaluateUpdate()
        {
            if (isOverthinking)
            {
                EvaluateOverthinking();
                return;
            }

            if (!isHolding) return;

            currentChargeTimer += Time.deltaTime;

            if (currentChargeTimer >= chargeTime)
            {
                isOverthinking = true;
                isHolding = false;
                ResetTimers();
                return;
            }
            
            currentDelayTimer += Time.deltaTime;

            var targetDelay = Mathf.Clamp(outputDelay * outputDelayModifier.Evaluate(ChargeAlpha), 0.001f, float.MaxValue);

            bool shouldAnimate = false;
            
            while (currentDelayTimer > targetDelay)
            {
                currentDelayTimer -= targetDelay;
                OnBrainstorm?.Invoke();

                shouldAnimate = true;
            }

            if (!shouldAnimate) return;
            
            scaleTween?.Kill();
            scaleParent.localScale = Vector3.one * scaleFrom;
            scaleTween = scaleParent.DOScale(Vector3.one, scaleDuration).SetEase(scaleCurve);
        }

        private void EvaluateOverthinking()
        {
            currentOverthinkTimer += Time.deltaTime;

            if (currentOverthinkTimer < overthinkTime) return;
            isOverthinking = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (isOverthinking) return;
            
            isHolding = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isOverthinking) return;

            isHolding = false;
            ResetTimers();
        }

        private void ResetTimers()
        {
            currentDelayTimer = currentChargeTimer = 0f;
        }

        public void ApplyParameters(QuizParameters parameters)
        {
            
        }
    }
}