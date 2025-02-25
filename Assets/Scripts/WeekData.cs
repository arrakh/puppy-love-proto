using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Week Data")]
    public class WeekData : ScriptableObject
    {
        public DayData[] days;
    }
}