using LittleMars.Common;
using UnityEngine;

namespace LittleMars.Buildings.View
{
    public class BuildingViewIndicators : BuildingIndicator
    {
        [SerializeField] private SpriteRenderer[] _renderers;
        [SerializeField] private IndicatorsCatalogue _indicators;

        private void Awake()
        {
            if (_renderers == null || _renderers.Length == 0)
                _renderers = GetComponentsInChildren<SpriteRenderer>();
        }
        public override void UpdateIndicator(BStates state)
        {
            var sprite = _indicators.GetIndicator(state);
            for (int i = 0; i < _renderers.Length; i++)
                _renderers[i].sprite = sprite;
        }
    }
}
