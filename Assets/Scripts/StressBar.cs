using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace DefaultNamespace
{
    public class StressBar : MonoBehaviour
    {
        public Image moodImage;
        public Sprite[] moodSprites;
        public Slider slider;
        public float smoothTime;
        public ButtonScaleAnimation scaleAnim;

        private float target, current, currentVelocity;
        
        public void Set(int value)
        {
            moodImage.sprite = moodSprites[value];
            target = value;
            scaleAnim.StartAnimation();
        }

        private void Update()
        {
            current = Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime);
            slider.value = current;
        }
    }
}