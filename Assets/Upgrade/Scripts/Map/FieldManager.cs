using LittleMars.Common;
using LittleMars.Map.Routers;
using LittleMars.Map.States;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LittleMars.Map
{
    public class FieldManager
    {
        MapRouter _router;
        ResourceField<MapSlotExtended>.Factory _factory;
        MapRouterCheck.Factory _checkFactory;
        Settings _settings;

        public FieldManager(MapRouter router, ResourceField<MapSlotExtended>.Factory factory,
            MapRouterCheck.Factory checkFactory, Settings settings)
        {
            _router = router;
            _factory = factory;
            _settings = settings;
            _checkFactory = checkFactory;
        }

        public List<ResourceField<MapSlotExtended>> Fields(List<List<MapSlotExtended>> slots)
        {
            if (_settings.Mode == MapMode.none) return null;

            //Debug.Log("Build Resource fields");
            //Debug.Log("   Count: " + _settings.Fields.Length);
            var fields = new List<ResourceField<MapSlotExtended>>();

            using var check = _checkFactory.Create();
            for (int i = 0; i < _settings.Fields.Length; i++)
            {
                var resource = _settings.Fields[i].Type;
                var path = _settings.Fields[i].Amount;
                if (_router.TryBuildRouteFromRandom(path, slots, check,
                    out List<MapSlotExtended> route, out _))
                {
                    //Debug.Log($"  Build for: {resource}. Route Lenght { route.Count}.");
                    var field = _factory.Create();
                    field.Resource = resource;
                    field.Values = route.ToArray();

                    fields.Add(field);
                }
            }

            return fields;
        }

        [Serializable]
        public class Settings
        {
            public MapMode Mode;
            public ResourceUnit<Path>[] Fields;
        }
    }
}
