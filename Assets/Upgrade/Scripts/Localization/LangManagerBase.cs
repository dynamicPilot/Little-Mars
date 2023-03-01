using LittleMars.Common;
using UnityEngine;

namespace LittleMars.Localization
{
    public class LangManagerBase
    {
        readonly Langs _lang;
        readonly TagGroup _group;
        protected TextBlocks _blocks;

        public LangManagerBase(Langs lang, TagGroup group)
        {
            _lang = lang;
            _blocks = null;
        }

        public string GetText(string tag, TagGroup group)
        {
            Debug.Log("Get text....");
            if (!CheckBlockForGroup(group)) return "";

            _blocks.TryGetText(tag, _lang, out string text);
            Debug.Log("Get text...." + text);
            return text;
        }

        public string[] GetAllTagTexts(string tagPart, TagGroup group)
        {
            if (!CheckBlockForGroup(group)) return null;

            _blocks.TryGetAllTextWithTag(tagPart, _lang, out string[] texts); ;
            return texts;
        }

        bool CheckBlockForGroup(TagGroup group)
        {
            if (_blocks == null) return false;
            else if (group != _group) return false;

            return true;
        }

        protected virtual void GetBlocks()
        {
        }

    }
}
