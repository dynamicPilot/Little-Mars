using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.LevelMenus;
using LittleMars.UI.Achievements;
using LittleMars.UI.LevelMenus;
using LittleMars.UI.ResourceSlots;
using LittleMars.UI.SlotUIFactories;
using LittleMars.WindowManagers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameStateLevelMenuUI : MenuUIWithControls
{
    [Header("Resources Info")]
    [SerializeField] RectTransform _resourcesSlotParent;

    [Header("Goals Info")]
    [SerializeField] RectTransform _goalsSlotParent;

    GameStateLevelMenu _stateMenu;
    bool _isSlotCreated = false;
    [Inject]
    public void Constructor(GameStateLevelMenu stateMenu, SoundsForGameMenuUI sounds)
    {
        _stateMenu = stateMenu;
        _gameMenu = stateMenu;
        _sounds = sounds;
        _isSlotCreated = false;
        Init();
    }

    void Init() => SetButtons();

    private void OnDestroy()
    {
        RemoveListeners();
    }

    public override void OnOpenMenu(WindowContext context)
    {
        if (!_isSlotCreated) CreateSlots();
        base.OnOpenMenu(context);
    }

    void CreateSlots()
    {
        Debug.Log("GameStateLevelMenuUI: create slots");
        _isSlotCreated = true;
        _stateMenu.CreateSlots(_resourcesSlotParent, _goalsSlotParent);
    }

    protected override void Close()
    {
        // signal that close
        base.Close();
    }

}
