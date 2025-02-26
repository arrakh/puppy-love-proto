using System;
using DG.Tweening;
using UnityEngine;

namespace Utilities
{
    public class ScaleLoopAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform toScale;
        [SerializeField] private float from = 1.2f;
        [SerializeField] private float to = 1f;
        [SerializeField] private float duration = 0.3f;
        [SerializeField] private AnimationCurve scaleCurve;
        [SerializeField] private LoopType loopType = LoopType.Restart;
        [SerializeField] private bool playOnAwake = false;

        private Tween scaleTween;

        private void Awake()
        {
            if (playOnAwake) StartAnimation();
        }

        public void StartAnimation()
        {
            if (scaleTween != null) DOTween.Kill(scaleTween);
            
            toScale.transform.localScale = Vector3.one * from;
            scaleTween = toScale.DOScale(to, duration).SetEase(scaleCurve).SetLoops(-1, loopType);
        }

        public void StopAnimation()
        {
            scaleTween?.Kill();
        }
    }
}