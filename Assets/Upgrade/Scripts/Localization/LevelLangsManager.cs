using LittleMars.Common;
using LittleMars.Configs;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Localization
{
    public class LevelLangsManager : LangManagerBase, IInitializable, ILevelLangManager
    {    
        readonly JsonConverter _jsonConverter;
        readonly LevelSettings _levelSettings;
        
        public LevelLangsManager(LevelSettings levelSettings,
            JsonConverter jsonConverter, LangSettings playerSettings)
            : base(playerSettings, TagGroup.level)
        {
            _levelSettings = levelSettings;
            _jsonConverter = jsonConverter;
        }

        public void Initialize()
        {            
            GetBlocks();
        }
        protected override void GetBlocks()
        {       
            _blocks = _jsonConverter.FromJsonTextAsset<TextBlocks>(_levelSettings.Blocks);
        }

        [Serializable]
        public class LevelSettings
        {
            public TextAsset Blocks;
        }
    }
}
