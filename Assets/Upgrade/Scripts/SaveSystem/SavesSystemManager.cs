using System;
using UnityEngine;
using Zenject;

namespace LittleMars.SaveSystem
{
    public class SavesSystemManager : IInitializable
    {       
        readonly SavesSystem.Factory _savesSystemFactory;
        readonly JsonDataSaver.Factory _jsonSaverFactory;
        readonly DataLoaderFactory _loaderFactory;

        public SavesSystemManager(SavesSystem.Factory savesSystemFactory, JsonDataSaver.Factory jsonSaverFactory, 
            DataLoaderFactory loaderFactory)
        {
            _savesSystemFactory = savesSystemFactory;
            _jsonSaverFactory = jsonSaverFactory;
            _loaderFactory = loaderFactory;
        }

        public void Initialize()
        {
            LoadData();
        }

        void LoadData()
        {
            var saveSystem = SetSavesSystem();
            var data = saveSystem.LoadData();

            if(data == null)
            {
                Debug.Log("Null data!");
            }
        }

        SavesSystem SetSavesSystem()
        {
            ISaver saver = GetSaver();
            ILoader loader = GetLoader();

            return _savesSystemFactory.Create(saver, loader);
        }

        ISaver GetSaver()
        {
            return _jsonSaverFactory.Create();
        }

        ILoader GetLoader()
        {
            return _loaderFactory.CreateLoader();
        }

        [Serializable]
        public class Settings
        {
            public string FolderName;
            public string FileName;
            public string BackupFileName;
            public string JsonFileEx;
            public string BinaryFileEx;
        }
    }
}
