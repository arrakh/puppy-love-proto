using DG.Tweening;
using UnityEngine;

namespace Utilities
{
    public class ButtonScaleAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform toScale;
        [SerializeField] private float from = 1.2f;
        [SerializeField] private float to = 1f;
        [SerializeField] private float duration = 0.3f;
        [SerializeField] private AnimationCurve scaleCurve;

        private Tween scaleTween;
        
        public void StartAnimation()
        {
            if (scaleTween != null) DOTween.Kill(scaleTween);
            
            toScale.transform.localScale = Vector3.one * from;
            scaleTween = toScale.DOScale(to, duration).SetEase(scaleCurve);
        }
    }
}