using System.Collections.Generic;

namespace LittleMars.Common.Signals
{
    public struct ResourcesBalanceUpdatedSignal
    {
        public Dictionary<Resource, float> ResourcesBalance;
    }

    public struct ResourcesProductionChangedSignal
    {
        public Dictionary<Resource, Dictionary<Period, float>> Production;
    }

    public struct ResourcesNeedsChangedSignal
    {
        public Dictionary<Resource, float> Needs;
    }
}
