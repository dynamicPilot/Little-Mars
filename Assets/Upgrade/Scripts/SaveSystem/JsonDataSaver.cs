using LittleMars.Localization;
using UnityEngine;
using Zenject;

namespace LittleMars.SaveSystem
{
    public class JsonDataSaver : ISaver
    {
        readonly PlayerDataProvider _dataProvider;
        readonly JsonConverter _converter;
        readonly SavesSystemPathConstructor _pathConstructor;

        public JsonDataSaver(PlayerDataProvider dataProvider,
            SavesSystemPathConstructor pathConstructor, JsonConverter converter)
        {
            _dataProvider = dataProvider;
            _pathConstructor = pathConstructor;
            _converter = converter;
        }

        public void SaveData()
        {
            var needSave = _dataProvider.GetData(out var data);
            Debug.Log("Json saver: need save? " + needSave);
            if (!needSave) return;

            Debug.Log("Json saver: save new data " + data.CurrentLevel);
            _pathConstructor.TryGetMainPath(out var mainPath);
            _converter.ToJson(data, mainPath);
        }

        public class Factory : PlaceholderFactory<JsonDataSaver>
        { }
    }
}
