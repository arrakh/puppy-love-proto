using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class AnswerButton : MonoBehaviour
    {
        public Button button;
        public TextMeshProUGUI answerText;
        public Image background;
        
        public void Set(Color color, string answer, Action<string> onAnswerButton)
        {
            background.color = color;
            answerText.text = answer;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => { onAnswerButton?.Invoke(answer); });
        }
    }
}