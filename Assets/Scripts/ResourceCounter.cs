using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace DefaultNamespace
{
    public class ResourceCounter : MonoBehaviour
    {
        public GameObject[] countObjects;
        public Image fill;
        public float smoothTime;
        public ButtonScaleAnimation scaleAnim;

        private float target, current, currentVelocity;
        
        public void SetCount(float count)
        {
            target = count;
            scaleAnim.StartAnimation();
        }

        private void Update()
        {
            current = Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime);
            SetCountInternal(current);
        }

        private void SetCountInternal(float count)
        {
            var countFloored = Mathf.Ceil(count) - 1;

            for (int i = 0; i < countObjects.Length; i++)
            {
                bool shouldBeOn = i < countFloored;
                countObjects[i].SetActive(shouldBeOn);
            }

            fill.fillAmount = count - countFloored;
        }
    }
}