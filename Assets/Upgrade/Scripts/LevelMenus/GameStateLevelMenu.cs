using LittleMars.Commands.Level;
using LittleMars.Model.GoalDisplays;
using LittleMars.Model.TimeUpdate;
using LittleMars.UI.GoalSlots;
using LittleMars.UI.ResourceSlots;
using UnityEngine;
using Zenject;

namespace LittleMars.LevelMenus
{
    public class GameStateLevelMenu : LevelMenu
    {
        ResourcesBalanceMenuManager _resourcesManager;
        GoalSlotMenuManager _goalManager;

        public GameStateLevelMenu(LevelCommandManager commandManager, SignalBus signalBus,
            ResourcesBalanceMenuManager resourcesManager, GoalSlotMenuManager goalManager,
            TimeSpeedManager timeSpeedManager, GoalDisplayStrategiesManager manager) 
            : base(commandManager, timeSpeedManager, manager, signalBus)
        {
            _resourcesManager = resourcesManager;
            _goalManager = goalManager;
        }

        public void CreateSlots(RectTransform resourceParent, RectTransform goalParent)
        {
            _resourcesManager.CreateSlots(resourceParent);
            _goalManager.CreateSlots(goalParent);
        }

        public override void Open()
        {
            base.Open();
            _resourcesManager.OnOpenMenu();
            _goalManager.OnOpenMenu();
        }

    }
}
