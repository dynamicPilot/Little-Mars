using UnityEngine;
using Zenject;

namespace LittleMars.Buildings.View
{
    public class BuildingObjectView : MonoBehaviour
    {
        [SerializeField] private Transform[] _rotatedParts;
        [SerializeField] private BoxCollider2D[] _colliders;

        private void OnValidate()
        {
            ChangeCollidersState(false);
        }

        public void OnStart() => ChangeCollidersState(true);

        public void OnRemove() => ChangeCollidersState(false);


        public void RotateView(float angle)
        {
            for (int i = 0; i < _rotatedParts.Length; i++)
                _rotatedParts[i].Rotate(0f, 0f, angle);
        }

        private void ChangeCollidersState(bool state)
        {
            foreach (BoxCollider2D collider in _colliders)
                collider.enabled = state;
        }


        public class Factory : PlaceholderFactory<BuildingObjectView>
        {
        }

    }
}
