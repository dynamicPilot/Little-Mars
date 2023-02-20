using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Zenject;
using JsonConverter = LittleMars.Localization.JsonConverter;

namespace LittleMars.SaveSystem
{
    public class BinaryDataLoader : ILoader
    {
        readonly JsonConverter _converter;
        readonly SavesSystemPathConstructor _pathConstructor;
        readonly PlayerDataProvider _dataProvider;

        string _binaryPath;

        public BinaryDataLoader(JsonConverter converter, SavesSystemPathConstructor pathConstructor, 
            PlayerDataProvider dataProvider)
        {
            _converter = converter;
            _pathConstructor = pathConstructor;
            _dataProvider = dataProvider;

            _binaryPath = "";
        }

        public PlayerData LoadData()
        {
            PlayerData data = null;
            bool hasFile = _pathConstructor.TryGetBinaryPath(out _binaryPath);

            if (hasFile)
            {
                var gameData = LoadBinaryData();
                data = _dataProvider.GetPlayerDataByGameData(gameData);
            }
            else
            {
                data = _dataProvider.GetEmptyData();
            }

            ConvertToJson(data);
            return data;
        }

        GameData LoadBinaryData()
        {           
            var data = GetBinaryData();

            if (data != null)
            {
                DeleteBinaryData();
            }

            return data;
        }


        GameData GetBinaryData()
        {
            GameData data = null;
            using (var stream = File.OpenRead(_binaryPath))
            {
                var formatter = new BinaryFormatter();
                data = formatter.Deserialize(stream) as GameData;
            }

            return data;
        }

        void ConvertToJson(PlayerData data)
        {
            _pathConstructor.TryGetMainPath(out var mainPath);
            _converter.ToJson(data, mainPath);
        }

        void DeleteBinaryData()
        {
            _pathConstructor.TryGetBinaryPath(out var backUpPath);

            DeleteFile(_binaryPath);
            DeleteFile(backUpPath);
        }

        void DeleteFile(string path)
        {
            if (File.Exists(path)) File.Delete(path);
        }


        public class Factory: PlaceholderFactory<BinaryDataLoader>
        { }
    }
}
