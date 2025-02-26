using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class ResourceUI : MonoBehaviour
    {
        public GameController gameController;

        public ResourceCounter logicCounter, moodCounter;
        public StressBar stressBar;

        private void Awake()
        {
            gameController.onLogicUpdated += OnLogicUpdate;
            gameController.onMoodUpdated += OnMoodUpdate;
            gameController.onStressUpdated += OnStressUpdate;
        }

        public void SetIsGame(bool isGame)
        {
            stressBar.gameObject.SetActive(!isGame);
        }

        private void OnStressUpdate(int oldValue, int newValue) => stressBar.Set(newValue);

        private void OnMoodUpdate(float v) => moodCounter.SetCount(v);

        private void OnLogicUpdate(float v) => logicCounter.SetCount(v);
    }
}