using LittleMars.Common.Signals;
using LittleMars.Configs;
using Zenject;

namespace LittleMars.MainMenus
{
    public class SettingsMenu
    {
        PlayerConfigProvider _provider;
        SignalBus _signalBus;

        public SettingsMenu(PlayerConfigProvider provider, SignalBus signalBus)
        {
            _provider = provider;
            _signalBus = signalBus;
        }

        public PlayerConfig GetPlayerConfig()
        {
            return _provider.GetData();
        }

        public void SavePlayerConfig()
        {
            _signalBus.Fire<NeedSaveConfigSignal>();
        }
    }
}
