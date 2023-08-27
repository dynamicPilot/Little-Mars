using LittleMars.UI.Effects;
using LittleMars.UI.Tooltip;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.UI.BuildingsSlots
{
    public class BuildingSlotGameUI : MonoBehaviour
    {
        [Header("Connections Section")]
        [SerializeField] private RectTransform _connectionsSlotParent;
        [SerializeField] private RectTransform _tooltipControllerParent;

        [Header("List Screens Section")]
        [SerializeField] private ListScreenEffectUI _buildingCostScreen;
        [SerializeField] private ListScreenEffectUI _productionCostScreen;
        [SerializeField] private ListScreenEffectUI _needsCostScreen;

        public ListScreenEffectUI BuildingCostSlotParent { get => _buildingCostScreen; }
        public ListScreenEffectUI ProductionCostSlotParent { get => _productionCostScreen; }
        public ListScreenEffectUI NeedsCostSlotParent { get => _needsCostScreen; }
        public RectTransform ConnectionsSlotParent { get => _connectionsSlotParent; }
        public RectTransform TooltipControllerParent { get => _tooltipControllerParent; }
    }
}
