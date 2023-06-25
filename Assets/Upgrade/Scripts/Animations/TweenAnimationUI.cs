using DG.Tweening;
using UnityEngine;

namespace LittleMars.Animations
{
    public class TweenAnimationUI : TweenAnimation
    {
        [Header("Animation id")]
        [SerializeField] protected bool _useId = false;
        [SerializeField] protected string _id = string.Empty;
        [SerializeField] bool _startOnEnable = true;
        private void OnEnable()
        {
            if (_startOnEnable) StartAnimation();
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
