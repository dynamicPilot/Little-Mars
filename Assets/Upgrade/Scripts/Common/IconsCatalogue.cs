using System;
using UnityEngine;

namespace LittleMars.Common
{
    public class IconsCatalogue
    {
        readonly Settings _settings;

        public IconsCatalogue(Settings settings)
        {
            _settings = settings;
        }


        public Sprite ResourceIcon(Resource type)
        {
            return _settings.Catalogue.Icon((int)type, IconType.resource);
        }

        public Sprite BuildingIcon(BuildingType type)
        {
            Debug.Log("Need buildingIcon " + type);
            return _settings.Catalogue.Icon((int)type, IconType.building);
        }

        [Serializable]
        public class Settings
        {
            public Catalogue Catalogue;
        }
    }
}
