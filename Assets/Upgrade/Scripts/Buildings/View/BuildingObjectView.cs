using LittleMars.Common;
using UnityEngine;
using Zenject;

namespace LittleMars.Buildings.View
{
    public class BuildingObjectView : MonoBehaviour
    {
        [SerializeField] private BuildingIndicator[] _viewIndicators;
        [SerializeField] private Transform[] _rotatedParts;
        [SerializeField] private BoxCollider2D _collider;

        private void OnValidate()
        {
            _collider.enabled = false;
        }

        public void OnStart()
        {
            _collider.enabled = true;
        }

        public void OnRemove()
        {
            _collider.enabled = false;
        }

        public void RotateView(float angle)
        {
            for (int i = 0; i < _rotatedParts.Length; i++)
                _rotatedParts[i].Rotate(0f, 0f, angle);
        }

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
