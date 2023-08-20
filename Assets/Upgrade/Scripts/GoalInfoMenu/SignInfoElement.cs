using LittleMars.Common;
using UnityEngine;

namespace LittleMars.GoalInfoMenu
{
    public class SignInfoElement
    {
        public Sprite Icon { get; private set; }
        public Size Size { get; private set; }
        public string Text { get; private set; }
        public SignInfoElement(Sprite icon, Size size, string text)
        {
            Icon = icon;
            Size = size;
            Text = text;
        }

    }
}
