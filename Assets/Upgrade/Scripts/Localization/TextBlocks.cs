using LittleMars.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LittleMars.Localization
{
    [Serializable]
    public class TextBlocks
    {
        public string Title { private get; set; }
        public Dictionary<string, TextBlock[]> Blocks { private get; set; }

        public TextBlocks(string title)
        {
            Title = title;
            Blocks = new Dictionary<string, TextBlock[]>();
        }

        public void AddBlock(string tag, TextBlock[] blocks)
        {
            Blocks.TryAdd(tag, blocks);
        }

        public void TryGetText(string tag, int index, out string text)
        {
            text = "";

            if (!Blocks.ContainsKey(tag)) return;

            Debug.Log($"Has this lang {index} ? " + (index < Blocks[tag].Length));
            text = (index < Blocks[tag].Length) ? Blocks[tag][index].Text :
                Blocks[tag][0].Text;
        }

        public void TryGetAllTextWithTag(string tagPart, int index, out string[] texts)
        {
            texts = null;

            var test = from tag in Blocks.Keys
                       where tag.Contains(tagPart)
                       select (index < Blocks[tag].Length) ? Blocks[tag][index].Text : Blocks[tag][0].Text;

            texts = test.ToArray();
        }

    }

    [Serializable]
    public class TextBlock
    {
        public int Lang { get; set; }
        public string Text { get; set; }

        public TextBlock(int lang, string text)
        {
            Lang = lang;
            Text = text;
        }


    }
}
