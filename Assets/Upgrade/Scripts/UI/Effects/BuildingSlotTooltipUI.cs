using LittleMars.Common;
using LittleMars.UI.BuildingsSlots;
using LittleMars.UI.SlotUIFactories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;
using Debug = UnityEngine.Debug;
using LittleMars.UI.Tooltip;

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

        string[] GetTagsForFields()
        {
            return null;
        }

        string[] GetTagsForConnections()
        {
            return null;
        }
    }
}