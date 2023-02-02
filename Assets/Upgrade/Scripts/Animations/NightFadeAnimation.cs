using DG.Tweening;
using UnityEngine;

namespace LittleMars.Animations
{
    public class NightFadeAnimation : TweenAnimation
    {
        [SerializeField] private SpriteRenderer _renderer;
        [Header("Settings")]
        [SerializeField] private float _fadeDuration;
        [SerializeField] private float _dayValue;
        [SerializeField] private float _nightValue;

        public void ToDayState()
        {
            DoFade(_dayValue);
        }

        public void ToNightState()
        {
            DoFade(_nightValue);
        }

        private void DoFade(float endValue)
        {
            _renderer.DOFade(endValue, _fadeDuration).SetEase(Ease.InOutSine);
        }
    }
}
