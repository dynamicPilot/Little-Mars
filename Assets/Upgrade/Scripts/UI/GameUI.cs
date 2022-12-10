using System;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _mainCanvas;
        [SerializeField] private Transform _buildingSlotParent;

        public RectTransform MainCanvas { get => _mainCanvas; }
        public Transform BuildingSlotParent() => _buildingSlotParent;
        
    }
}
