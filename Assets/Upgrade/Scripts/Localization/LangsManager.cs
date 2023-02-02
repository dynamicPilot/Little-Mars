using LittleMars.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LittleMars.Localization
{
    public class LangsManager : IInitializable
    {
        readonly Langs _lang;
        readonly JsonConverter _jsonConverter;
        readonly SceneSettings _sceneSettings;
        readonly LevelSettings _levelSettings;

        Dictionary<TagGroup, TextBlocks> _blocks;
        public LangsManager(Langs lang, SceneSettings sceneSettings, LevelSettings levelSettings,
            JsonConverter jsonConverter)
        {
            _lang = lang;
            _sceneSettings = sceneSettings;
            _levelSettings = levelSettings;
            _jsonConverter = jsonConverter;
        }

        public void Initialize()
        {
            GetBlocks();
            TestForGetAllTexts();
        }

        public string GetText(string tag, TagGroup group)
        {
            Debug.Log("Try get text for " + tag + " " + group);
            if (!CheckBlockForGroup(group)) return "";
            
            _blocks[group].TryGetText(tag, _lang, out string text);

            return text;
        }

        public string[] GetAllTagTexts(string tagPart, TagGroup group)
        {
            Debug.Log("Try get text for " + tagPart + " " + group);

            if (!CheckBlockForGroup(group)) return null;

            _blocks[group].TryGetAllTextWithTag(tagPart, _lang, out string[] texts); ;
            return texts;
        }

        private bool CheckBlockForGroup(TagGroup group)
        {
            if (_blocks == null)
            {
                Debug.Log("Null blocks");
                return false;
            }
            else if (!_blocks.ContainsKey(group))
            {
                Debug.Log("No key");
                return false;
            }

            return true;
        }

        private void TestForGetAllTexts()
        {
            var result = GetAllTagTexts("goal", TagGroup.level);

            if (result == null) Debug.Log("Empty result");
            else
            {
                for (int i = 0; i < result.Length; i++)
                    Debug.Log("  text " + result[i]);
            }
        }

        private void GetBlocks()
        {
            _blocks = new Dictionary<TagGroup, TextBlocks>();
            _blocks.Add(TagGroup.scene, _jsonConverter.FromJsonTextAsset<TextBlocks>(_sceneSettings.Blocks));
            _blocks.Add(TagGroup.level, _jsonConverter.FromJsonTextAsset<TextBlocks>(_levelSettings.Blocks));
        }

        [Serializable]
        public class SceneSettings
        {
            public TextAsset Blocks;
        }

        [Serializable]
        public class LevelSettings
        {
            public TextAsset Blocks;
        }
    }
}
