using LittleMars.Common;
using System;

namespace LittleMars.Rockets
{
    [Serializable]
    public class Rocket
    {
        public int ArrivedHour;
        public int DepartureHour;
        public TradeUnit<float>[] TradesWhenDepart;
    }
}
