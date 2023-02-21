using LittleMars.Common.Signals;
using System;
using Zenject;

namespace LittleMars.StartMenus
{
    public class LangMenu
    {
        readonly SignalBus _signalBus;

        PlayerConfig _config;
        bool _needUpdate;

        public LangMenu(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _config = null;
            _needUpdate = false;
        }

        public void Open(PlayerConfig config)
        {
            _config = config;
        }

        public void SetLangIndex(int index)
        {
            // set lang
            if (_config == null) return;

            _needUpdate = _config.Lang == index;
            if (_needUpdate) _config.Lang = index;
        }

        public void Close()
        {
            if (_config == null) return;
            _signalBus.Fire(new ConfigIsLoadedSignal { PlayerConfig = _config, NeedUpdate = _needUpdate });
        }
    }
}
