using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "School Class Data")]
    public class SchoolClassData : EventData
    {
        public QuizEntries entries;
        public QuizParameters parameters;
    }
}