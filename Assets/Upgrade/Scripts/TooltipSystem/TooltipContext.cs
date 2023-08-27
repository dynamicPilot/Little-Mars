using LittleMars.Common;
using UnityEngine;

namespace LittleMars.TooltipSystem
{
    public class TooltipContext
    {
        public TooltipContext(Direction direction, float offset, RectTransform target, string text = "")
        {
            Direction = direction;
            Offset = offset;
            Target = target;
            Text = text;
        }

        public Direction Direction { get; private set; }
        public float Offset { get; private set; }
        public RectTransform Target { get; private set; }
        public string Text { get; private set; }
    }
}
