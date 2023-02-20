using LittleMars.Localization;
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
            var data = _dataProvider.GetData();
            if (data == null) return;

            _pathConstructor.TryGetMainPath(out var mainPath);
            _converter.ToJson(data, mainPath);
        }

        public class Factory : PlaceholderFactory<JsonDataSaver>
        { }
    }
}
