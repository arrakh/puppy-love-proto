using System;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ink
{
    public class InkTextController : MonoBehaviour
    {
        [FormerlySerializedAs("inkController")] [SerializeField] private InkStoryController inkStoryController;
        [SerializeField] private float letterPause;

        private Coroutine typeCoroutine;
        private string targetText;
        private string historyText;
        private bool isTyping;
        
        public event Action OnTextFinished;
        public event Action<string> OnTextDisplay; 

        public bool IsTyping => isTyping;
        
        private void OnEnable() => inkStoryController.OnStoryText += OnText;
        private void OnDisable() => inkStoryController.OnStoryText -= OnText;

        private void OnText(string text)
        {
            Stop();
            targetText = text;
            typeCoroutine = StartCoroutine(TypeSentence());
        }

        public void Finish()
        {
            Stop();
            /*historyText += targetText;
            lineText.text = historyText;*/
            OnTextDisplay?.Invoke(targetText);
            OnTextFinished?.Invoke();
        }

        private void Stop()
        {
            if (typeCoroutine != null) StopCoroutine(typeCoroutine);
            isTyping = false;
        }

        IEnumerator TypeSentence()
        {
            isTyping = true;
            //lineText.text = historyText;
            OnTextDisplay?.Invoke(String.Empty);
            string currentText = String.Empty;

            // This regex pattern will match any TMP tags.
            string pattern = @"<.*?>";

            string[] parts = Regex.Split(targetText, pattern);

            int currentIndex = 0;
            foreach (string part in parts)
            {
                if (Regex.IsMatch(part, pattern))
                {
                    // If the part is a TMP tag, append it whole without delay.
                    currentText += part;
                    OnTextDisplay?.Invoke(historyText + currentText);
                }
                else
                {
                    // If the part is normal text, reveal it letter by letter.
                    foreach (char letter in part)
                    {
                        currentText += letter;
                        currentIndex++;
                        OnTextDisplay?.Invoke(historyText + currentText);
                        yield return new WaitForSeconds(letterPause);
                    }
                }
            }
            
            Finish();
        }
    }
}