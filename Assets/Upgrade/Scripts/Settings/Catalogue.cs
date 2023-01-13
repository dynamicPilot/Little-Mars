using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LittleMars.Common
{
    [CreateAssetMenu(menuName = "LittleMars/Catalogue")]
    public class Catalogue : ScriptableObject
    {
        [SerializeField] private ResourceIconsSettings Icons;
        private Dictionary<Resource, Sprite> _resourceIcons = null;

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

        private void CreateResourceDictionary()
        {
            _resourceIcons = new Dictionary<Resource, Sprite>();
            _resourceIcons.Add(Resource.energy, Icons.Energy);
            _resourceIcons.Add(Resource.supply_units, Icons.Supply);
            _resourceIcons.Add(Resource.food, Icons.Food);
            _resourceIcons.Add(Resource.metalls, Icons.Metals);
            _resourceIcons.Add(Resource.machines, Icons.Machines);
            _resourceIcons.Add(Resource.goods, Icons.Goods);
            _resourceIcons.Add(Resource.money, Icons.Money);
        }

        public Sprite ResourceIcon(Resource resource)
        {
            if (_resourceIcons == null) CreateResourceDictionary();
            _resourceIcons.TryGetValue(resource, out var icon);
            return icon;
        }

    }
}
