using LittleMars.Common;
using UnityEngine;
using Zenject;

namespace LittleMars.Buildings.View
{
    public class BuildingObjectView : MonoBehaviour
    {
        [SerializeField] private BuildingIndicator[] _viewIndicators;

        public void TransitToState(BStates state)
        {
            UpdateIndicators(state);
        }

        private void UpdateIndicators(BStates state)
        {
            for (int i = 0; i < _viewIndicators.Length; i++)
                _viewIndicators[i].UpdateIndicator(state);
        }

        public class Factory : PlaceholderFactory<BuildingObjectView>
        { 
        }

    }
}
