using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image sliderFill;

        private float timer, currentTimer;

        public void Set(float time)
        {
            timer = currentTimer = time;
        }

        private void Update()
        {
            currentTimer -= Time.deltaTime;
            
            var alpha = Mathf.Clamp01(currentTimer / timer);
            sliderFill.fillAmount = alpha;
            sliderFill.color = gradient.Evaluate(alpha);
        }

        public bool IsCompleted() => currentTimer < 0f;
    }
}