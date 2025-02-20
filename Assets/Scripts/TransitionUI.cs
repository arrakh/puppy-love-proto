using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TransitionUI : MonoBehaviour
    {
        public CanvasGroup fadeGroup;

        public TextMeshProUGUI titleText;
        
        public void TransitionIn(string title, float duration)
        {
            titleText.text = title;

            fadeGroup.alpha = 0f;
            fadeGroup.DOFade(1f, duration);
        }

        public IEnumerator WaitTransitionIn(string title, float duration, float stayDuration = 0f)
        {
            TransitionIn(title, duration);
            yield return new WaitForSeconds(duration + stayDuration);
        }

        public void TransitionOut(float duration)
        {
            fadeGroup.DOFade(0f, duration);
        }

        public IEnumerator WaitTransitionOut(float duration, float stayDuration = 0f)
        {
            TransitionOut(duration);
            yield return new WaitForSeconds(duration + stayDuration);
        }
    }
}