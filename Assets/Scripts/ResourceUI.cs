using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class ResourceUI : MonoBehaviour
    {
        public GameController gameController;
        
        //todo: should be separate Counter objects
        public TextMeshProUGUI logicText, moodText;

        private void Update()
        {
            logicText.text = gameController.logic.ToString("F1");
            moodText.text = gameController.mood.ToString("F1");
        }
    }
}