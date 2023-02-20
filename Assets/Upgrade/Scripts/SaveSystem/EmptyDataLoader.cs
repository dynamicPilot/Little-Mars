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
            _pathConstructor.TryGetMainPath(out var path);
            _converter.ToJson(data, path);
            return data;
        }

        public class Factory : PlaceholderFactory<EmptyDataLoader>
        { }

    }
}
