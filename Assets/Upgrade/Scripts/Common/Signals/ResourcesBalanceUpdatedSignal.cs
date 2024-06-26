﻿using System.Collections.Generic;

namespace LittleMars.Common.Signals
{
    public struct ResourcesBalanceUpdatedSignal
    {
        public Dictionary<Resource, float> ResourcesBalance;
    }

    //public struct ResourcesProductionChangedSignal
    //{
    //    public Dictionary<Resource, Dictionary<Period, float>> Production;
    //    public Period Period;
    //}

    //public struct ResourcesNeedsChangedSignal
    //{
    //    public Dictionary<Resource, float> Needs;
    //}

    public struct TotalProductionChangedSignal
    {
        public Dictionary<Resource, float> TotalProdution;
    }
}
