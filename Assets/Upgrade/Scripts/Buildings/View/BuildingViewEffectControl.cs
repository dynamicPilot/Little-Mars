using UnityEngine;

namespace LittleMars.Buildings.View
{
    public interface IAnimationIsOver
    {
        void AnimationIsOver();
    }

    public class BuildingViewEffectControl : MonoBehaviour, IAnimationIsOver
    {
        [SerializeField] BuildingViewEffectAnimation _animation;
        [SerializeField] BuildingObjectViewFacade _facade;
        bool _animationIsRunning;

        public void MakeEffect()
        {
            if (_animationIsRunning) return;

            _animationIsRunning = true;
            _animation.DoAnimation(this);
        }

        public void AnimationIsOver()
        {
            _animationIsRunning = false;
            _facade.EffectIsOver();
        }
    }
}
