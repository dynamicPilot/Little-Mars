using LittleMars.Animations;
using LittleMars.Buildings.View.Indicators;
using UnityEngine;

namespace LittleMars.Buildings.View
{
    public class BuildingObjectViewComponents : MonoBehaviour
    {
        [SerializeField] private IndicatorsControl _indicators;
        [SerializeField] private BuildingAnimationsControl _animations;
        [SerializeField] private BuildingViewEffectControl _effects;
        public IndicatorsControl Indicators { get => _indicators; }
        public BuildingAnimationsControl Animations { get => _animations; }

        public void MakeEffect(bool isStart)
        {
            _effects.MakeEffect(isStart);
        }

        public bool CanMakeEffect()
        {
            return _effects != null;
        }

        public void AddToId(string suffix)
        {
            _indicators?.AddToId(suffix);
        }
    }
}
