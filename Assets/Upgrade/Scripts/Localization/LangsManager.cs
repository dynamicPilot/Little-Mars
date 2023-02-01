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

        Dictionary<TagGroup, TextBlocks> _blocks;
        public LangsManager(Langs lang, SceneSettings sceneSettings, JsonConverter jsonConverter)
        {
            _lang = lang;
            _sceneSettings = sceneSettings;
            _jsonConverter = jsonConverter;
        }

        public void Initialize()
        {
            GetBlocks();
        }

        public string GetText(string tag, TagGroup group)
        {
            Debug.Log("Try get text for " + tag + " " + group);
            string text = "";

            if (_blocks == null)
            {
                Debug.Log("Null blocks");
                return text;
            }
            else if (!_blocks.ContainsKey(group))
            {
                Debug.Log("No key");
                return text;
            }

            _blocks[group].TryGetText(tag, _lang, ref text);

            return text;
        }

        private void GetBlocks()
        {
            _blocks = new Dictionary<TagGroup, TextBlocks>();
            _blocks.Add(TagGroup.scene, _jsonConverter.FromJsonTextAsset<TextBlocks>(_sceneSettings.Blocks));
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
