using LittleMars.Common;
using LittleMars.UI.Effects;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.UI.BuildingSlots
{
    public class BuildingSlotView : MonoBehaviour
    {
        [SerializeField] private CounterSlotUI _amount;
        [SerializeField] private Image _shape;
        [SerializeField] private Image _building;

        [Inject]
        public void Constructor(BuildingObject building)
        {
            SetBuildingView(building);
        }

        public void UpdateAmount(int amount)
        {
            _amount.UpdateCounter(amount);
        }

        private void SetBuildingView(BuildingObject building)
        {
            _shape.sprite = building.View.Shape;
            _building.sprite = building.View.Sprite;
        }

    }
}
