using UnityEngine;

namespace LittleMars.UI.BuildingsSlots
{
    public class BuildingSlotGameUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _resourceSlotParent;
        [SerializeField] private RectTransform _connectionsSlotParent;

        public RectTransform ResourceSlotParent { get => _resourceSlotParent; }
        public RectTransform ConnectionsSlotParent { get => _connectionsSlotParent; }
    }
}
