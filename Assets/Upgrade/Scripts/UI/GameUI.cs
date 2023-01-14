using UnityEngine;

namespace LittleMars.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _mainCanvas;
        [SerializeField] private Transform _buildingSlotParent;
        [SerializeField] private RectTransform _resourceSlotParent;
        [SerializeField] private RectTransform _resourceBalanceSlotParent;

        public RectTransform MainCanvas { get => _mainCanvas; }
        public Transform BuildingSlotParent() => _buildingSlotParent;
        public RectTransform ResourceSlotParent { get => _resourceSlotParent; }
        public RectTransform ResourceBalanceSlotParent { get => _resourceBalanceSlotParent; }

    }
}
