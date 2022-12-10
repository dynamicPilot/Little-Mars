using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.Common
{
    public class BuildingCatalogue
    {
        readonly Settings _settings;

        public BuildingCatalogue(Settings settings)
        {
            _settings = settings;
        }

        public BuildingObject BuildingObject(BuildingType type, Size size)
        {
            return _settings.Catalogue.Dome.Small;
        }

        [Serializable]
        public class Settings
        {
            public Catalogue Catalogue;
        }
    }
}
