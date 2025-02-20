using System;

namespace DefaultNamespace
{
    [Serializable] 
    public class QuizParameters //this is a class and not a struct just so we can have default values
    {
        public float baseTimer = 20f;
        public int quizCount = 3;
        public int passingGrade = 2;
        public int otherAnswerCount = 4;
        public int fakeAnswerCount = 3;
        public int colorCount = 4;
        public int brainstormCount = 2;
        public float drainPerFocus = 1.3f;
        public float classLogicToMoodRatio = 0.7f;
    }
}