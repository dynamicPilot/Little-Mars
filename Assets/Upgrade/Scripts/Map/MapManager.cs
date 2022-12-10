using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.Map.Routers;
using LittleMars.Map.States;
using LittleMars.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

namespace LittleMars.Map
{
    public class MapManager : IInitializable
    {
        List<List<MapSlotExtended>> _slots = new List<List<MapSlotExtended>>();
        public List<List<MapSlotExtended>> Slots { get => _slots; }

        int _rows, _columns;

        readonly Settings _settings;
        readonly MapSlotFactory _slotFactory;
        readonly SignalBus _signalBus;
        

        public MapManager(Settings settings, MapSlotFactory slotFactory, SignalBus signalBus)
        {
            _slotFactory = slotFactory;
            _settings = settings;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            CreateMapSlots();
        }

        private void CreateMapSlots()
        {
            _slots = _slotFactory.CreateSlots(_settings.Mode);
            _rows = Slots.Count;
            _columns = Slots[0].Count;

            Debug.Log($"Create slots. Rows {_columns}, Columns {_columns}.");
            _signalBus.Fire<MapSlotsAreReadySignal>();
        }

        public void AddBuildingToSlots(IEnumerable<Indexes> indexes, BuildingType type)
        {
            foreach (Indexes index in indexes)
                _slots[index.Row][index.Column].ChangeBuilding(type);
        }


        [Serializable]
        public class Settings
        {
            public int Rows;
            public int Columns;
            public BuildingType[] BuildingTypes;
            public MapMode Mode;
            public CustomMapSettings CustomMap;
        }

    }
}
