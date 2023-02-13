using LittleMars.Common;
using LittleMars.Rockets;
using UnityEngine;

namespace LittleMars.Models
{
    public class TradeManager
    {
        readonly ProductionManager _production;

        public TradeManager(ProductionManager production)
        {
            _production = production;
        }

        public void MakeTradeByRocket(Rocket rocket)
        {
            for (int i = 0; i < rocket.TradesWhenDepart.Length; i++)
                MakeTrade(rocket.TradesWhenDepart[i]);
        }

        void MakeTrade(TradeUnit<float> unit)
        {
            var needs = new ResourceUnit<float>[] { unit.Need };
            if (!_production.HasResources(needs)) return;

            _production.SignleTrade(unit);
        }

    }
}
