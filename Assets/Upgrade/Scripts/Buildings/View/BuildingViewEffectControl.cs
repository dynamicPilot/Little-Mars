using LittleMars.Animations;
using UnityEngine;

namespace LittleMars.Buildings.View
{
    public class BuildingViewEffectControl : MonoBehaviour, IAnimationCallback
    {
        [SerializeField] BuildingViewEffectAnimation _animation;

        bool _animationIsRunning;

        public void MakeEffect(bool isStart)
        {
            if (_animationIsRunning) return;

            _animationIsRunning = true;
            if (isStart)_animation.OnStartEffectAnimation(this);
            else _animation.OnEndEffectAnimation(this);
        }

        public void OnAnimationCallback()
        {
            _animationIsRunning = false;
        }
    }
}
