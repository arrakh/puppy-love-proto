using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class BrainstormAnswer : MonoBehaviour
    {
        public int brainstormCount = 3;
        
        public QuizController quizController;
        public Button brainstormButton;
        public AnswerButton prefab;
        public RectTransform holder;

        private void Start()
        {
            brainstormButton.onClick.AddListener(OnBrainstorm);
        }

        private void OnBrainstorm()
        {
            Clear();

            var answers = quizController.GetAnswerPool()
                .OrderBy(_ => Random.value).Take(brainstormCount);

            foreach (var answer in answers)
            {
                var button = Instantiate(prefab, holder);

                var isAnswer = answer.Equals(quizController.CurrentAnswer);

                //var color = isAnswer ? quizController.GetCurrentColor() : quizController.GetRandomColor();
                var color = Color.white;
                
                button.Set(color, answer, a => quizController.Answer(a)); 
            }
        }

        private void Clear()
        {
            foreach (Transform child in holder)
                Destroy(child.gameObject);
        }
    }
}