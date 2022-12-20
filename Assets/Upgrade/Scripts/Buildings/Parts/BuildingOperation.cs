using LittleMars.Common;
using LittleMars.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

namespace LittleMars.Buildings.Parts
{
    /// <summary>
    /// Class for control the buiding operation and production.
    /// </summary>
    public class BuildingOperation
    {
        readonly BuildingObject.OperationSettings _settings;

        Dictionary<Resource, Dictionary<Period, float>> _production;
        List<Resource> _needs;
        public Dictionary<Resource, Dictionary<Period, float>> Production { 
            get
            {
                if (_production == null) FillProduction();
                return _production;
            }
        }

        public BuildingOperation(BuildingObject buildingObject)
        {
            _settings = buildingObject.Operation;
            _production = null;
            _needs = null;
        }

        //public BuildingOperation(BuildingCatalogue cataloque, BuildingType type, Size size)
        //{
        //    _settings = cataloque.BuildingObject(type, size).Operation;
        //    _production = null;
        //}

        public bool HasNeedForThisResource(Resource resource)
        {
            if (_needs == null) FillNeeds();
            return _needs.Contains(resource);
        }

        public ResourceUnit<float>[] Needs() => _settings.Needs;

        private void FillProduction()
        {
            _production = new Dictionary<Resource, Dictionary<Period, float>>();
            ProductionForPeriod(Period.day, _settings.DayProduction);
            ProductionForPeriod(Period.night, _settings.NightProduction);
        }

        private void FillNeeds()
        {
            _needs = new List<Resource>();

            for (int i = 0; i < _settings.Needs.Length; i++)
            {
                _needs.Add(_settings.Needs[i].Type);
            }
        }

        private void ProductionForPeriod(Period period, ResourceUnit<float>[] production)
        {
            for (int i = 0; i < production.Length; i++)
            {
                var resource = production[i].Type;
                if (!_production.ContainsKey(resource)) _production[resource] = new Dictionary<Period, float>();
                if (!_production[resource].ContainsKey(period)) _production[resource][period] = production[i].Amount;
            }
        }
    }


    ///// <summary>
    ///// Settings class for BuildingOperation.
    ///// </summary>
    //[Serializable]
    //public class BuildingOperationSettings
    //{
    //    public Priority Priority;
    //    public ResourceUnit<float>[] DayProduction;
    //    public ResourceUnit<float>[] NightProduction;
    //    public BuildingType[] Connections;
    //    public ResourceUnit<float>[] Needs;
    //}
}
