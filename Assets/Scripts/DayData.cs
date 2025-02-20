using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Day Data")]
    public class DayData : ScriptableObject
    {
        public string dayName;
        public SchoolClassData[] classes;
    }
}