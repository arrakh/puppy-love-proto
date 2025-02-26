using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ActivityButton : MonoBehaviour
    {
        public ActivityData data;
        public Button button;
        public Image bgImage;
        public TextMeshProUGUI text;
        public RectTransform[] allGain, logicGains, moodGains, stressGains;
        public Sprite[] sprites;
        public Vector2 rotationRange;
        
        public string Id => data.activityId;

        public void Display(ActivityData activityData)
        {
            data = activityData;
            text.text = data.displayName;

            bgImage.sprite = sprites[Random.Range(0, sprites.Length)];
            transform.localRotation = Quaternion.Euler(0,0, Random.Range(rotationRange.x, rotationRange.y));
            
            foreach (var e in allGain)
                e.gameObject.SetActive(false);
            
            if (data.logicGain != 0) logicGains[GetGainIndex(data.logicGain)].gameObject.SetActive(true);
            if (data.moodGain != 0) moodGains[GetGainIndex(data.moodGain)].gameObject.SetActive(true);
            if (data.stressGain != 0) stressGains[GetGainIndex(data.stressGain)].gameObject.SetActive(true);
        }

        private int GetGainIndex(int gain)
        {
            int max = 2;
            var clamped = Mathf.Clamp(gain, -max, max);
            return clamped + max - 1;
        }
    }
}