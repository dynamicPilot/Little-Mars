using LittleMars.Common;
using LittleMars.Configs;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

namespace LittleMars.Localization
{
    public class LangManagerBase
    {
        readonly LangSettings _settings;
        readonly TagGroup _group;
        protected TextBlocks _blocks;

        public LangManagerBase(LangSettings settings, TagGroup group)
        {
            _settings = settings;
            _group = group;
            _blocks = null;
        }

        public string GetText(string tag, TagGroup group)
        {
            //Debug.Log("Get text....");
            if (!CheckBlockForGroup(group)) return "";

            _blocks.TryGetText(tag, _settings.LangIndex, out string text);
            //Debug.Log("Get text...." + text);
            return text;
        }

        public string[] GetAllTagTexts(string tagPart, TagGroup group)
        {
            if (!CheckBlockForGroup(group)) return null;

            _blocks.TryGetAllTextWithTag(tagPart, _settings.LangIndex, out string[] texts); ;
            return texts;
        }

        bool CheckBlockForGroup(TagGroup group)
        {
            //Debug.Log("LangManagerBase: check block for group...");
            if (_blocks == null) return false;
            else if (group != _group) return false;

            return true;
        }

        protected virtual void GetBlocks()
        {
        }

    }
}
