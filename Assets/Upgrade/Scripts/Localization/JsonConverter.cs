using LittleMars.Common;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using Zenject;

namespace LittleMars.Localization
{
    public class JsonConverter
    {
        public void ToJson<T>(T obj, string filePath, Formatting formatting = Formatting.Indented) where T : class
        {
            string jsonString = JsonConvert.SerializeObject(obj, formatting);
            File.WriteAllText(filePath, jsonString);
        }

        public T FromJson<T>(string filePath) where T : class
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public T FromJsonTextAsset<T>(TextAsset asset) where T : class
        {
            string jsonString = asset.ToString();
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        private void TestRun()
        {
            Debug.Log("JsonConverter: test run....");
            TextBlock tBlock0 = new TextBlock((int)Langs.en, "to-to-to");
            TextBlock tBlock1 = new TextBlock((int)Langs.ru, "то-то-то");

            var blocks = new TextBlocks("test");
            blocks.AddBlock("test_button", new TextBlock[2] { tBlock0, tBlock1 });

            string path = Application.persistentDataPath + "/" + "Test" + "/" + "textBlock.json";
            ToJson<TextBlocks>(blocks, path);
        }

    }
}
