using System;

namespace LittleMars.Common
{
    public enum PathType
    {
        single,
        multiple
    }

    [Serializable]
    public class PathUnit
    {
        public Direction Direction;
        public bool IsBackStep;

        public Direction RotateBySeveralTimes(int counter)
        {
            var direction = ((int)Direction + counter) % 4;
            return (Direction) direction;
        }
    }

    [Serializable]
    public class Path
    {
        public PathType Type;
        public PathUnit[] Units;
    }
}
