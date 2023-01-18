using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Common
{
    [CreateAssetMenu(menuName = "LittleMars/Catalogue")]
    public class Catalogue : ScriptableObject
    {
        [SerializeField] private ResourceIconsSettings Resources;
        [SerializeField] private BuildingIconsSettings Buildings;

        private Dictionary<int, Sprite> _resourceIcons = null;
        private Dictionary<int, Sprite> _buildingIcons = null;

        [Serializable]
        public class ResourceIconsSettings
        {
            public Sprite Energy;
            public Sprite Supply;
            public Sprite Food;
            public Sprite Metals;
            public Sprite Machines;
            public Sprite Goods;
            public Sprite Money;
        }

        [Serializable]
        public class BuildingIconsSettings
        {
            public Sprite Dome;
            public Sprite SolarPlant;
            public Sprite SupplyUnit;
            public Sprite Farm;
            public Sprite Mine;
            public Sprite Factory;
            public Sprite Workshop;
        }

        private void CreateResourceDictionary()
        {
            _resourceIcons = new Dictionary<int, Sprite>();
            _resourceIcons.Add((int) Resource.energy, Resources.Energy);
            _resourceIcons.Add((int) Resource.supply_units, Resources.Supply);
            _resourceIcons.Add((int) Resource.food, Resources.Food);
            _resourceIcons.Add((int) Resource.metalls, Resources.Metals);
            _resourceIcons.Add((int) Resource.machines, Resources.Machines);
            _resourceIcons.Add((int) Resource.goods, Resources.Goods);
            _resourceIcons.Add((int) Resource.money, Resources.Money);
        }

        private void CreateBuildingDictionary()
        {
            _buildingIcons = new Dictionary<int, Sprite>();

            _buildingIcons.Add((int)BuildingType.dome, Buildings.Dome);
            _buildingIcons.Add((int)BuildingType.power_plant, Buildings.SolarPlant);
            _buildingIcons.Add((int)BuildingType.supply_plant, Buildings.SupplyUnit);
            _buildingIcons.Add((int)BuildingType.farm, Buildings.Farm);
            _buildingIcons.Add((int)BuildingType.mine, Buildings.Mine);
            _buildingIcons.Add((int)BuildingType.factory, Buildings.Factory);
            _buildingIcons.Add((int)BuildingType.workshop, Buildings.Workshop);

        }

        public Sprite Icon(int index, IconType type)
        {
            if (type == IconType.resource)
            {
                if (_resourceIcons == null) CreateResourceDictionary();
                return GetIcon(index, _resourceIcons);
            }
            else if (type == IconType.building)
            {
                if (_buildingIcons == null) CreateBuildingDictionary();
                return GetIcon(index, _buildingIcons);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private Sprite GetIcon(int index, Dictionary<int, Sprite> dict)
        {
            dict.TryGetValue(index, out var icon);
            return icon;
        }

    }
}
