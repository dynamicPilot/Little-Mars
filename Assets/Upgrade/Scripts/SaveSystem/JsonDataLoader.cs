using Zenject;
using JsonConverter = LittleMars.Localization.JsonConverter;

namespace LittleMars.SaveSystem
{
    public class JsonDataLoader : ILoader
    {
        readonly JsonConverter _converter;
        readonly SavesSystemPathConstructor _pathConstructor;
        readonly PlayerDataProvider _dataProvider;
        public JsonDataLoader(JsonConverter converter, SavesSystemPathConstructor pathConstructor, 
            PlayerDataProvider dataProvider)
        {
            _converter = converter;
            _pathConstructor = pathConstructor;
            _dataProvider = dataProvider;
        }

        public PlayerData LoadData()
        {
            PlayerData data = null;

            var hasFile = _pathConstructor.TryGetMainPath(out var path);

            if (hasFile)
                data = _converter.FromJson<PlayerData>(path);
            else
                data = _dataProvider.GetEmptyData();

            return data;
        }

        public class Factory : PlaceholderFactory<JsonDataLoader>
        { }
    }
}
