using System;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [Serializable] 
    public class QuizParameters //this is a class and not a struct just so we can have default values
    {
        public float baseTimer = 60f;
        public int quizCount = 3;
        public int passingGrade = 2;
        public int otherAnswersCount = 1;
        public int fakeAnswersCount = 0;
        public float drainPerFocus = 1.3f;
        public float classLogicToMoodRatio = 0.7f;
    }
}