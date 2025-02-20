using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class BrainstormAnswer : MonoBehaviour
    {
        public int brainstormCount = 3;
        public float logic = 10f;
        public float mood = 10f;
        public float drainPerFocus = 1.3f;
        public float classLogicToMoodRatio = 0.7f;
        
        public QuizController quizController;
        public Button brainstormButton;
        public Button focusButton;
        public AnswerButton prefab;
        public RectTransform holder;
        public TextMeshProUGUI logicText, moodText;

        private List<AnswerButton> spawnedAnswers = new();
        private List<AnswerButton> spawnedDistractions = new();

        private void Start()
        {
            brainstormButton.onClick.AddListener(OnBrainstorm);
            focusButton.onClick.AddListener(OnFocus);
        }

        private void OnFocus()
        {
            var logicCost = drainPerFocus * classLogicToMoodRatio;
            var moodCost = drainPerFocus * (1 - classLogicToMoodRatio);

            if (logic < logicCost || mood < moodCost) return;

            logic -= logicCost;
            mood -= moodCost;

            ClearDistractions();
            
            focusButton.interactable = !(logic < logicCost || mood < moodCost);
        }

        private void Update()
        {
            logicText.text = logic.ToString("F1");
            moodText.text = mood.ToString("F1");
        }

        private void OnBrainstorm()
        {
            var answers = quizController.GetAnswerPool()
                .OrderBy(_ => Random.value).Take(brainstormCount);

            foreach (var answer in answers)
            {
                var button = Instantiate(prefab, holder);

                var isAnswer = answer.Item1.Equals(quizController.CurrentAnswer);

                var color = isAnswer ? quizController.GetCurrentColor() : quizController.GetRandomColor();

                bool isDistraction = answer.Item2;
                button.Set(isDistraction, holder, color, answer.Item1, a => quizController.Answer(a)); 
                
                if (isDistraction) spawnedDistractions.Add(button);
                else spawnedAnswers.Add(button);
            }
        }

        public void ClearAnswers()
        {
            foreach (var button in spawnedAnswers)
                Destroy(button.gameObject);
            
            spawnedAnswers.Clear();
        }

        private void ClearDistractions()
        {
            foreach (var button in spawnedDistractions)
                Destroy(button.gameObject);
            
            spawnedDistractions.Clear();
        }

        public void ClearAll()
        {
            ClearAnswers();
            ClearDistractions();
        }
    }
}