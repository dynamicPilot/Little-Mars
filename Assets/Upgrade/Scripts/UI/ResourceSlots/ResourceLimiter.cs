using System;
using System.Collections.Generic;
using TMPro;
using Zenject;
using Resource = LittleMars.Common.Resource;

namespace LittleMars.UI.ResourceSlots
{
    public class ResourceLimiter : IInitializable
    {
        readonly Settings _settings;
        Dictionary<Resource, float> _limits;

        public ResourceLimiter(Settings settings)
        {
            _settings = settings;
            _limits = new Dictionary<Resource, float>();
        }

        public void Initialize()
        {
            CreateDict();
        }

        public bool IsLimit(Resource type, float amount)
        {
            if (!_limits.ContainsKey(type)) return false;

            return _limits[type] >= amount;
        }

        void CreateDict()
        {
            for (int i = 0; i < (int)Resource.all; i++)
            {
                var resource = (Resource)i;
                if (resource == Resource.food)
                    _limits[resource] = _settings.FoodLimit;
                else if (resource == Resource.metalls)
                    _limits[resource] = _settings.MetalsLimit;
                else if (resource == Resource.machines)
                    _limits[resource] = _settings.MachinesLimit;
                else
                    _limits[resource] = _settings.GeneralLimit;
            }
        }

        [Serializable]
        public class Settings
        {
            public float GeneralLimit;
            public float FoodLimit;
            public float MetalsLimit;
            public float MachinesLimit;
        }
    }
}
