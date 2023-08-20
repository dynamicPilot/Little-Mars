using LittleMars.Common;
using LittleMars.Configs;
using System;
using UnityEngine;
using Zenject;

namespace LittleMars.Localization
{

    public class LangManager : LangManagerBase, IInitializable
    {
        readonly JsonConverter _jsonConverter;
        readonly ProjectSettings _projectSettings;

        public LangManager(JsonConverter jsonConverter, ProjectSettings sceneSettings,
            LangSettings playerSettings) : base(playerSettings, TagGroup.scene)
        {
            _jsonConverter = jsonConverter;
            _projectSettings = sceneSettings;
        }

        public void Initialize()
        {
            GetBlocks();
        }

        protected override void GetBlocks()
        {
            _blocks = _jsonConverter.FromJsonTextAsset<TextBlocks>(_projectSettings.Blocks);
        }

        [Serializable]
        public class ProjectSettings
        {
            public TextAsset Blocks;
        }
    }
}
