using UnityEngine;

namespace LittleMars.Animations.CircularScreen
{
    public class RocketMoveAnimation : RectMoveAnimation
    {
        [Header("Flip Animation")]
        [SerializeField] FlipAnimation _flipAnimation;

        public override void StartAnimation()
        {
            _flipAnimation.StartAnimation();
            base.StartAnimation();
        }
    }
}
