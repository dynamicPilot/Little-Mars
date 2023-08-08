using LittleMars.AudioSystems;
using LittleMars.Common;
using LittleMars.Common.Signals;
using LittleMars.LevelMenus;
using LittleMars.UI.LevelMenus;
using LittleMars.WindowManagers;
using UnityEngine;
using Zenject;

public class GameStateLevelMenuUI : MenuUIWithControls
{
    [Header("Resources Info")]
    [SerializeField] RectTransform _resourcesSlotParent;

    [Header("Goals Info")]
    [SerializeField] RectTransform _goalsSlotParent;

    GameStateLevelMenu _stateMenu;
    SignalBus _signalBus;
    bool _isSlotCreated = false;
    [Inject]
    public void Constructor(GameStateLevelMenu stateMenu, SoundsForGameMenuUI sounds, SignalBus signalBus)
    {
        _stateMenu = stateMenu;
        _gameMenu = stateMenu;
        _sounds = sounds;
        _isSlotCreated = false;
        _signalBus = signalBus;
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
        // signal
        _signalBus.TryFire(new WindowIsClosedSignal { MenuState = (int)MenuState.state });
        base.Close();
    }

}
