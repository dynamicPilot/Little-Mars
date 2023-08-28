using LittleMars.Common;
using LittleMars.UI.BuildingsSlots;
using Zenject;
using Debug = UnityEngine.Debug;
using LittleMars.UI.Tooltip;
using System.Collections.Generic;

namespace LittleMars.UI.Effects
{
    public class BuildingSlotTooltipUI: IInitializable
    {
        readonly BuildingObject _building;
        readonly BuildingSlotGameUI _gameUI;
        readonly BuildingSlotTooltipControllerFactory _factory;

        bool _hasFields = false;
        bool _hasConnections = false;

        public BuildingSlotTooltipUI(BuildingSlotGameUI gameUI, BuildingObject building,
            BuildingSlotTooltipControllerFactory factory)
        {
            _gameUI = gameUI;
            _building = building;
            _factory = factory;

            _hasFields = _building.Construction.ResourcesInMap.Length > 0;
            _hasConnections = _building.Operation.Connections.Length > 0;
        }

        public void Initialize()
        {
            // надо сделать через фабрику, чтобы избежать ошибки в концепции DI
            CreateSlots();
        }
        void CreateSlots()
        {
            // если слотов с соединениями или полями нет, то выключить контейнер
            _gameUI.TooltipControllerParent.gameObject.SetActive(_hasConnections || _hasFields);

            if (!(_hasFields || _hasConnections)) return;

            // если есть, то при создании пробросить тэги для текстов и тип (если тип вообще нужен)
            var type = (_hasConnections) ? TooltipType.connections : TooltipType.fields;
            var tags = GetTags();

            //Debug.Log("TextUI: tags count is " + tags.Length + " type " + type);
            _factory.CreateSlot(tags, type, _gameUI.TooltipControllerParent);
        }
        string[] GetTags()
        {
            if (_hasConnections && _hasFields)
                Debug.Log("BuildingSlotTooltipUI : has fields and connections !!");

            if (_hasConnections) return GetTagsForConnections();
            else if (_hasFields) return GetTagsForFields();
            else return null;
        }

        string[] GetTagsForConnections()
        {
            var tags = new List<string>();
            for (int i = 0; i < _building.Operation.Connections.Length; i++)
            {
                var type = _building.Operation.Connections[i];
                //Debug.Log("    add connection type to " + type);
                tags.Add(type.ToString());
            }
            return tags.ToArray();
        }

        string[] GetTagsForFields()
        {
            var tags = new List<string>();
            for (int i = 0; i < _building.Construction.ResourcesInMap.Length; i++)
            {
                var type = _building.Construction.ResourcesInMap[i];
                //Debug.Log("    add field type to " + type);
                tags.Add(type.ToString());
            }
            return tags.ToArray();
        }
    }
}