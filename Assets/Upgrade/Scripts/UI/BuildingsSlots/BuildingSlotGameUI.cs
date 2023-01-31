using LittleMars.UI.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.UI.BuildingsSlots
{
    public class BuildingSlotGameUI : MonoBehaviour
    {        
        [SerializeField] private RectTransform _connectionsSlotParent;

        [Header("List Screens")]
        [SerializeField] private ListScreenEffectUI _buildingCostScreen;
        [SerializeField] private ListScreenEffectUI _productionCostScreen;
        [SerializeField] private ListScreenEffectUI _needsCostScreen;

        public ListScreenEffectUI BuildingCostSlotParent { get => _buildingCostScreen; }
        public ListScreenEffectUI ProductionCostSlotParent { get => _productionCostScreen; }
        public ListScreenEffectUI NeedsCostSlotParent { get => _needsCostScreen; }

        public RectTransform ConnectionsSlotParent { get => _connectionsSlotParent; }
    }
}
