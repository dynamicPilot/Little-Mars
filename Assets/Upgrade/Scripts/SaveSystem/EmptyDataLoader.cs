using UnityEngine;
using Zenject;
using JsonConverter = LittleMars.Localization.JsonConverter;

namespace LittleMars.SaveSystem
{
    public class EmptyDataLoader : ILoader
    {
        readonly PlayerDataProvider _dataProvider;
        readonly JsonConverter _converter;
        readonly SavesSystemPathConstructor _pathConstructor;

        public EmptyDataLoader(PlayerDataProvider dataProvider, JsonConverter converter, 
            SavesSystemPathConstructor pathConstructor)
        {
            _dataProvider = dataProvider;
            _converter = converter;
            _pathConstructor = pathConstructor;
        }

        public PlayerData LoadData()
        {
            var data = _dataProvider.GetEmptyData();
            var res =_pathConstructor.TryGetMainPath(out var path);
            // Debug.Log("EmptyDataLoader: get path for new data " + path + " is exist " + res);
            // TODO: add message when res is false
            _converter.ToJson(data, path);
            return data;
        }

        public class Factory : PlaceholderFactory<EmptyDataLoader>
        { }

    }
}
