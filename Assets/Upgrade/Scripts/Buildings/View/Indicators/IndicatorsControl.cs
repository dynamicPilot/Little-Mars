using LittleMars.Animations;
using LittleMars.Common;
using UnityEngine;

namespace LittleMars.Buildings.View.Indicators
{
    public class IndicatorsControl : MonoBehaviour
    {
        [SerializeField] BuildingIndicator[] _viewIndicators;

        public void UpdateState(BStates state)
        {
            for (int i = 0; i < _viewIndicators.Length; i++)
                _viewIndicators[i].UpdateIndicator(state);
        }

        public void AddToId(string suffix)
        {
            //_indicatorsAnimation?.AddToId(suffix);
        }
    }
}
