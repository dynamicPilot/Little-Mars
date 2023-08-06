using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.Animations
{
    public class TweenAnimationObject : TweenAnimation
    {
        [Header("Animation id")]
        [SerializeField] bool _useIdsWhenStop = true;
        [SerializeField] protected string _id = string.Empty;
        [SerializeField] bool _startOnEnable = true;

        protected List<string> _ids = new List<string>();
        private void OnEnable()
        {
            if (_startOnEnable) StartAnimation();
        }

        protected override void OnDisable()
        {
            StopAnimation();
        }
        public virtual void StartAnimation()
        {
        }

        public virtual void StopAnimation()
        {            
            if (_ids.Count > 0 && _useIdsWhenStop)
            {
                foreach (var id in _ids)
                    DOTween.Kill(id);
            }
            else
            {
                DOTween.Kill(_id);
            }
        }
    }
}
