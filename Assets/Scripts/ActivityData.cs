using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Activity Data")]
    public class ActivityData : EventData
    {
        public string activityId;
        public string logicBonusDisplay;
        public string moodBonusDisplay;
        
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