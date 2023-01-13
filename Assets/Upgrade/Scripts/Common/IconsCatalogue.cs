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
            return _settings.Catalogue.ResourceIcon(type);
        }

        [Serializable]
        public class Settings
        {
            public Catalogue Catalogue;
        }
    }
}
