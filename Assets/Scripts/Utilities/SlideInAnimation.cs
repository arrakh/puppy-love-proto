using DG.Tweening;
using UnityEngine;

namespace Utilities
{
    public class SlideInAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform toAnimate;
        [SerializeField] private bool playOnEnable;
        [SerializeField] private float localYOffset = -100f;
        [SerializeField] private float duration = 0.4f;
        [SerializeField] private AnimationCurve animationCurve;
 
        private Tween tween;

        public void StartAnimation()
        {
            if (tween != null) DOTween.Kill(tween);

            var pos = toAnimate.localPosition;
            pos.y += localYOffset;
            toAnimate.localPosition = pos;

            tween = toAnimate.DOLocalMoveY(0f, duration).SetEase(animationCurve);
        }

        private void OnEnable()
        {
            if (playOnEnable) StartAnimation();
        }
    }
}