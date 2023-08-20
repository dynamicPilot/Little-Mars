using LittleMars.Common;
using LittleMars.Common.Catalogues;
using LittleMars.Common.LevelGoal;
using LittleMars.Localization;
using LittleMars.Model;
using LittleMars.Settings;
using System;
using System.Collections.Generic;
using Zenject;

namespace LittleMars.GoalInfoMenu
{
    /// <summary>
    /// Class for sign info preparation for sign info ui in goal info menu.
    /// </summary>
    public class SignInfoMenu : IInitializable
    {
        readonly InfoLangManager _infoLangManager;
        readonly IconsCatalogue _iconCatalogue;
        readonly GoalsManager.Settings _goalSettings;

        bool _hasTimer = false;
        bool _hasStorage = false;
        bool _hasProduction = false;
        bool _hasBuildings = false;

        List<SignInfoElement> _elements = new List<SignInfoElement>();
        SignInfoElement[] _standardElements = null;

        public SignInfoMenu(InfoLangManager infoLangManager, 
            IconsCatalogue iconCatalogue, GoalsManager.Settings goalSettings)
        {
            _infoLangManager = infoLangManager;
            _iconCatalogue = iconCatalogue;
            _goalSettings = goalSettings;
        }

        public void Initialize()
        {
            CreateElements();
        }

        void CreateElements()
        {
            _hasTimer = _goalSettings.HasBuildingGoalsWithTimer;
            _hasStorage = _goalSettings.HasResourcesGoals;
            _hasProduction = _goalSettings.HasProductionGoals;
            _hasBuildings = _goalSettings.HasBuildingGoals || _goalSettings.HasBuildingGoalsWithTimer;

            if (_goalSettings.HasBuildingGoals) 
                GetElementFromBuildingGoals(_goalSettings.BuildingGoals);

            if (_goalSettings.HasBuildingGoalsWithTimer) 
                GetElementFromBuildingGoalsWithTimer(_goalSettings.BuildingGoalsWithTimers);

            CreateStandardElements();
        }

        void GetElementFromBuildingGoals(Goals<BuildingUnit<int>> goals)
        {
            foreach (var goal in goals.GoalsArray) CreateSignInfoElements(goal);
        }

        void GetElementFromBuildingGoalsWithTimer(GoalsWithTimer<BuildingUnit<int>> goals)
        {
            foreach (var goal in goals.GoalsArray) CreateSignInfoElements(goal);
        }

        void CreateSignInfoElements(Goal<BuildingUnit<int>> goal)
        {
            var type = goal.Unit.Type;
            var text = string.Format("{0} {1}", 
                _infoLangManager.GetInfo(goal.Unit.Size.ToString()),
                _infoLangManager.GetInfo(type.ToString()));

            var element = new SignInfoElement(_iconCatalogue.BuildingIcon(type),
                goal.Unit.Size, text);

            _elements.Add(element);
        }

        void CreateStandardElements()
        {
            _standardElements = new SignInfoElement[3] { null, null, null };
            var types = new GoalType[3] { GoalType.time, GoalType.resources, GoalType.production };
            
            for(int i = 0; i < types.Length; i++)
            {
                var element = new SignInfoElement(_iconCatalogue.GoalTypeIcon(types[i]),
                Size.small, _infoLangManager.GetInfo(types[i].ToString()));
                _standardElements[i] = element;
            }
            
        }
        public SignInfoElement[] GetSignInfoElements(out bool hasBuildings)
        {
            hasBuildings = _hasBuildings;
            return _elements.ToArray();
        }

        public SignInfoElement[] GetStandardElements(out bool[] standardStates)
        {
            standardStates = new bool[3] { _hasTimer, _hasStorage, _hasProduction };
            return _standardElements;
        }

    }
}
