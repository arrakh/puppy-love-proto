using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "Quiz Entries")]
    public class QuizEntries : ScriptableObject
    {
        public Quiz[] entries;
        public string[] fakeAnswers;
    }
}