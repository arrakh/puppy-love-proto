using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class DialogueTextBox : MonoBehaviour
    {
        public TextMeshProUGUI text;

        public float popupDuration;
        public AnimationCurve popupCurve;

        private Tween popupTween;
        
        public void PopupAnimation()
        {
            popupTween?.Kill();

            transform.localScale = Vector3.zero;
            popupTween = transform.DOScale(Vector3.one, popupDuration).SetEase(popupCurve);
        }

        public void Display(string toDisplay)
        {
            text.text = toDisplay;
        }
    }
}