using LittleMars.Animations;
using UnityEngine;

namespace LittleMars.UI
{
    public class GameAnimations : MonoBehaviour
    {
        [SerializeField] private NightFadeAnimation _nightAnimation;

        public NightFadeAnimation NightAnimation { get => _nightAnimation; }

    }
}
