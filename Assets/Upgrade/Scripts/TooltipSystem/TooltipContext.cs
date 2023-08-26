using LittleMars.Common;
using UnityEngine;

namespace LittleMars.TooltipSystem
{
    public class TooltipContext
    {
        public TooltipContext(Direction direction, Vector2 position, string text)
        {
            Direction = direction;
            Position = position;
            Text = text;
        }

        public Direction Direction { get; private set; }
        public Vector2 Position { get; private set; }
        public string Text { get; private set; }
    }
}
