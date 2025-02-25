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
        public float distractionPercentChance = 20f;
        public int distractionCount = 1;
        public float drainPerFocus = 1.3f;
        public float classLogicToMoodRatio = 0.7f;

        public GameController gameController;
        public QuizController quizController;
        public BrainstormButton brainstormButton;
        public Button focusButton;
        public AnswerButton prefab;
        public RectTransform holder;
        public TextMeshProUGUI logicText, moodText;

        private List<AnswerButton> spawnedAnswers = new();
        private List<AnswerButton> spawnedDistractions = new();

        private void Start()
        {
            brainstormButton.OnBrainstorm += OnBrainstorm;
            focusButton.onClick.AddListener(OnFocus);
            UpdateFocusVisual();
        }

        private void OnFocus()
        {
            var logicCost = drainPerFocus * classLogicToMoodRatio;
            var moodCost = drainPerFocus * (1 - classLogicToMoodRatio);

            if (gameController.logic < logicCost || gameController.mood < moodCost) return;

            gameController.logic -= logicCost;
            gameController.mood -= moodCost;

            ClearDistractions();
            
            focusButton.interactable = !(gameController.logic < logicCost || gameController.mood < moodCost);
        }

        private void Update()
        {
            var logicCost = drainPerFocus * classLogicToMoodRatio;
            var moodCost = drainPerFocus * (1 - classLogicToMoodRatio);
            
            logicText.text = "-" + logicCost.ToString("F1");
            moodText.text = "-" + moodCost.ToString("F1");
        }

        private void OnBrainstorm()
        {
            var answer = quizController.GetAnswerPool()
                .OrderBy(_ => Random.value).First();

            var button = Instantiate(prefab, holder);

            button.Set(holder, answer.color, answer.text, a => quizController.Answer(a)); 
                
            spawnedAnswers.Add(button);

            var shouldSpawnDistraction = Random.value * 100f < distractionPercentChance;
            if (!shouldSpawnDistraction) return;

            var distractions = quizController.stressDistractions.OrderBy(_ => Random.value).Take(distractionCount);
            foreach (var distraction in distractions)
            {
                //TODO: TO PERSONALIZE DISTRACTION, SPAWN FROM SOMEWHERE ELSE, NOT ANSWER BUTTON
                var distractionBtn = Instantiate(prefab, holder);
                distractionBtn.Set(holder, new Color(1f, 0.43f, 0.47f), distraction, null);
                
                spawnedDistractions.Add(distractionBtn);
            }
            
            UpdateFocusVisual();
        }

        public void ClearAnswers()
        {
            foreach (var button in spawnedAnswers)
                Destroy(button.gameObject);
            
            spawnedAnswers.Clear();
        }

        private void UpdateFocusVisual()
        {
            focusButton.gameObject.SetActive(spawnedDistractions.Count > 0);
        }

        private void ClearDistractions()
        {
            foreach (var button in spawnedDistractions)
                Destroy(button.gameObject);
            
            spawnedDistractions.Clear();
            UpdateFocusVisual();
        }

        public void ClearAll()
        {
            ClearAnswers();
            ClearDistractions();
        }

        public void ApplyParameters(QuizParameters parameters)
        {
            drainPerFocus = parameters.drainPerFocus; 
            classLogicToMoodRatio = parameters.classLogicToMoodRatio;

            brainstormButton.ApplyParameters(parameters);
        }
    }
}