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
        public float drainPerFocus = 1.3f;
        public float classLogicToMoodRatio = 0.7f;

        public GameController gameController;
        public QuizController quizController;
        public BrainstormButton brainstormButton;
        public Button focusButton;
        public Image focusSlider;
        public AnswerButton prefab;
        public RectTransform holder;
        public GameObject focusUnavailable;

        private List<AnswerButton> spawnedAnswers = new();
        private List<AnswerButton> spawnedDistractions = new();

        private void Start()
        {
            brainstormButton.OnBrainstorm += OnBrainstorm;
            focusButton.onClick.AddListener(OnFocus);
            UpdateFocusVisual();
        }

        void Update()
        {
            var logicCost = drainPerFocus * classLogicToMoodRatio;
            var moodCost = drainPerFocus * (1 - classLogicToMoodRatio);

            focusUnavailable.SetActive(gameController.logic < logicCost || gameController.mood < moodCost);
        }

        private void OnFocus()
        {
            var logicCost = drainPerFocus * classLogicToMoodRatio;
            var moodCost = drainPerFocus * (1 - classLogicToMoodRatio);

            if (gameController.logic < logicCost || gameController.mood < moodCost) return;

            if (Mathf.Abs(logicCost) > 0f) gameController.AddLogic(-logicCost);
            if (Mathf.Abs(moodCost) > 0f) gameController.AddMood(-moodCost);

            ClearDistractions();
            
            focusButton.interactable = !(gameController.logic < logicCost || gameController.mood < moodCost);
        }

        private void OnBrainstorm()
        {
            var answer = quizController.GetAnswerPool()
                .OrderBy(_ => Random.value).First();

            var button = Instantiate(prefab, holder);

            button.Set(holder, answer.color, Color.white, answer.text, a => quizController.Answer(button, a)); 
                
            spawnedAnswers.Add(button);

            SpawnStressDistraction();
            SpawnLoveDistraction();

            UpdateFocusVisual();
        
            
        }

        private void SpawnLoveDistraction()
        {
            var loveChance = gameController.love * 0.12;

            var shouldSpawnDistraction = Random.value < loveChance;
            if (!shouldSpawnDistraction) return;

            var loveCount = Mathf.FloorToInt(gameController.love / 2f);
            var distractions = quizController.loveDistractions.OrderBy(_ => Random.value).Take(loveCount);
            foreach (var distraction in distractions)
            {
                //TODO: TO PERSONALIZE DISTRACTION, SPAWN FROM SOMEWHERE ELSE, NOT ANSWER BUTTON
                var distractionBtn = Instantiate(prefab, holder);
                distractionBtn.Set(holder, new Color(1f, 153/255f, 1f), Color.black, distraction, null);
                
                spawnedDistractions.Add(distractionBtn);
            }
        }

        private void SpawnStressDistraction()
        {
            var shouldSpawnDistraction = Random.value < GetDistractionChance(gameController.stressLevel);
            if (!shouldSpawnDistraction) return;

            var distractionCount = GetDistractionCount(gameController.stressLevel);
            var distractions = quizController.stressDistractions.OrderBy(_ => Random.value).Take(distractionCount);
            foreach (var distraction in distractions)
            {
                //TODO: TO PERSONALIZE DISTRACTION, SPAWN FROM SOMEWHERE ELSE, NOT ANSWER BUTTON
                var distractionBtn = Instantiate(prefab, holder);
                distractionBtn.Set(holder, new Color(1f, 0.43f, 0.47f), Color.black, distraction, null);
                
                spawnedDistractions.Add(distractionBtn);
            }
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

            focusSlider.fillAmount = parameters.classLogicToMoodRatio;

            brainstormButton.ApplyParameters(parameters);
        }

        private float GetDistractionChance(int level)
            => level switch
            {
                1 => 0.2f,
                2 => 0.5f,
                3 or 4 => 1f,
                _ => 0f
            };

        private int GetDistractionCount(int level)
            => level switch
            {
                1 => 1,
                2 => 1,
                3 => 2,
                4 => 3,
                _ => 0
            };
    }
}