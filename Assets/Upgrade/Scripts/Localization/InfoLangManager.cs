using LittleMars.Common;
using LittleMars.Configs;
using System;
using Zenject;
using UnityEngine;

namespace LittleMars.Localization
{
    /// <summary>
    /// Manager for sign explanations texts.
    /// </summary>
    public class InfoLangManager : LangManagerBase, IInitializable
    {
        readonly JsonConverter _jsonConverter;
        readonly Settings _infoTextSettings;

        public InfoLangManager(Settings infoTextSettings,
            JsonConverter jsonConverter, LangSettings playerSettings)
            : base(playerSettings, TagGroup.info)
        {
            _infoTextSettings = infoTextSettings;
            _jsonConverter = jsonConverter;
        }

        public void Initialize()
        {
            GetBlocks();
        }
        protected override void GetBlocks()
        {
            _blocks = _jsonConverter.FromJsonTextAsset<TextBlocks>(_infoTextSettings.Blocks);
        }

        [Serializable]
        public class Settings
        {
            public TextAsset Blocks;
        }
    }
}
