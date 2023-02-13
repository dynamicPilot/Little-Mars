using LittleMars.Animations;
using UnityEngine;

namespace LittleMars.Buildings.View
{
    public abstract class BuildingViewEffectAnimation : MonoBehaviour
    {
        public abstract void OnStartEffectAnimation(IAnimationCallback animationIsOver);
        public abstract void OnEndEffectAnimation(IAnimationCallback animationIsOver);
    }
}
