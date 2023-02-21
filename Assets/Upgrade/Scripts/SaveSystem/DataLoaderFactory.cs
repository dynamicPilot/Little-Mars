using System;
using UnityEngine;
using Zenject;

namespace LittleMars.SaveSystem
{
    public class DataLoaderFactory : IDisposable
    {
        readonly SavesSystemPathConstructor _pathConstructor;
        readonly JsonDataLoader.Factory _jsonLoaderFactory;
        readonly BinaryDataLoader.Factory _binaryLoaderFactory;
        readonly EmptyDataLoader.Factory _emptyLoaderFactory;

        public DataLoaderFactory(SavesSystemPathConstructor pathConstructor, 
            JsonDataLoader.Factory jsonLoaderFactory, BinaryDataLoader.Factory binaryLoaderFactory, 
            EmptyDataLoader.Factory emptyLoaderFactory)
        {
            _pathConstructor = pathConstructor;
            _jsonLoaderFactory = jsonLoaderFactory;
            _binaryLoaderFactory = binaryLoaderFactory;
            _emptyLoaderFactory = emptyLoaderFactory;
        }

        public ILoader CreateLoader()
        {
            bool hasFile = _pathConstructor.TryGetMainPath(out var _);
            Debug.Log("Has json file? " + hasFile);
            if (hasFile) return _jsonLoaderFactory.Create();
            
            hasFile = _pathConstructor.TryGetBinaryPath(out _);
            Debug.Log("Has binary file? " + hasFile);
            if (hasFile) return _binaryLoaderFactory.Create();

            return _emptyLoaderFactory.Create();
        }

        public void Dispose()
        {
        }

        public class Factory : PlaceholderFactory<DataLoaderFactory>
        { }
    }
}
