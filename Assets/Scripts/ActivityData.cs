using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Activity Data")]
    public class ActivityData : EventData
    {
        public string activityId;
        public int logicGain;
        public int moodGain;
        public int stressGain;

#if UNITY_EDITOR
        protected override void OnEventValidate()
        {
            base.OnEventValidate();
            
            if (string.IsNullOrWhiteSpace(activityId))
                activityId = displayName.Replace(" ", "_").ToLowerInvariant().Trim();
        }
#endif
    }
}