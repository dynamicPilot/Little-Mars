using LittleMars.Buildings.View;
using LittleMars.Common;
using UnityEngine;
using Zenject;

namespace Assets.Upgrade.Scripts.Buildings.View
{
    public class BuildingColorIndicator : BuildingIndicator
    {
        [SerializeField] private SpriteRenderer[] _renderers;
        
        ColorsCatalogue _colors;

        [Inject]
        public void Constructor(ColorsCatalogue colors)
        {
            _colors = colors;
        }

        private void Awake()
        {
            if (_renderers == null || _renderers.Length == 0)
                _renderers = GetComponentsInChildren<SpriteRenderer>();
        }
        public override void UpdateIndicator(BStates state)
        {
            if (_colors == null) return;

            var color = _colors.BStateColor(state);
            for (int i = 0; i < _renderers.Length; i++)
                _renderers[i].color = color;
        }
    }
}
