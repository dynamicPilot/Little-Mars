using System;
using UnityEngine;
using Zenject;

namespace LittleMars.SaveSystem
{
    public class SavesSystemManager : IInitializable, IDisposable
    {       
        readonly SavesSystem.Factory _savesSystemFactory;
        readonly JsonDataSaver.Factory _jsonSaverFactory;
        readonly DataLoaderFactory.Factory _loaderFactory;        

        public SavesSystemManager(SavesSystem.Factory savesSystemFactory, JsonDataSaver.Factory jsonSaverFactory, 
            DataLoaderFactory.Factory loaderFactory)
        {
            _savesSystemFactory = savesSystemFactory;
            _jsonSaverFactory = jsonSaverFactory;
            _loaderFactory = loaderFactory;
        }

        public void Initialize()
        {
            SetSavesSystem();
        }

        public void SetSavesSystem()
        {
            ISaver saver = GetSaver();
            ILoader loader = GetLoader();

            _savesSystemFactory.Create(saver, loader);
        }

        ISaver GetSaver()
        {
            return _jsonSaverFactory.Create();
        }

        ILoader GetLoader()
        {
            using var factory = _loaderFactory.Create();
            return factory.CreateLoader();
        }

        public void Dispose()
        {
            Debug.Log("Dispose SavesSystemManager");
        }

        public class Factory : PlaceholderFactory<SavesSystemManager>
        { }
    }
}
