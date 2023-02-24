using LittleMars.Common.Signals;
using Zenject;
using UnityEngine;

namespace LittleMars.SaveSystem
{
    public class SavesSystem
    {
        readonly ISaver _saver;
        readonly ILoader _loader;
        readonly SignalBus _signalBus;
        public SavesSystem(ISaver saver, ILoader loader, SignalBus signalBus)
        {
            _saver = saver;
            _loader = loader;
            _signalBus = signalBus;

            Init();
        }

        void Init()
        {
            _signalBus.Subscribe<StartLoadingSignal>(LoadData);
            _signalBus.Subscribe<NeedSaveDataSignal>(SaveData);
        }

        public void SaveData()
        {
            Debug.Log("Save Data");
            _saver.SaveData();
        }

        public void LoadData()
        {
            Debug.Log("Loading Data");
            var data = _loader.LoadData();
            CheckData(data);
        }

        void CheckData(PlayerData data)
        {
            if (data == null)
            {
                Debug.Log("Null data!");
            }
            else
                _signalBus.Fire(new DataIsLoadedSignal { PlayerData = data });
        }

        public class Factory : PlaceholderFactory<ISaver, ILoader, SavesSystem>
        { }
    }
}
