using DG.Tweening;
using UnityEngine;

namespace LittleMars.Animations
{
    public class TweenAnimationEffect : TweenAnimation
    {
        [Header("Animation id")]
        [SerializeField] protected string _id = string.Empty;
        [SerializeField] bool _startOnEnable = true;
        private void OnEnable()
        {
            if (_startOnEnable) StartAnimation();
        }

        public void Play()
        {
            DOTween.Play(_id);
        }
        public void Pause()
        {
            DOTween.Pause(_id);
        }

        protected override void OnDisable()
        {
            DOTween.Kill(_id);
        }
        public virtual void StartAnimation()
        {
        }
    }
}
