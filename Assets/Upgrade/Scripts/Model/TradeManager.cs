using LittleMars.Common;
using System.Collections.Generic;
using Zenject;

namespace LittleMars.Models
{
    public class TradeManager : IInitializable
    {
        Dictionary<int, Dictionary<Resource, float>> _trades = null;

        public void Initialize()
        {
            _trades = new Dictionary<int, Dictionary<Resource, float>>();
        }

        //public bool TryGetTradesForHour(int hour, out Dictionary<Resource, float> trades)
        //{
        //    trades = null;
        //    if (!_trades.ContainsKey(hour)) return false;

        //    trades = _trades[hour];
        //    return true;
        //}

        //public float GetResourceTradeForHour(int hour, Resource resource)
        //{
        //    var amount = 0f;

        //    if (_trades.ContainsKey(hour))
        //    {
        //        if (_trades[hour].ContainsKey(resource))
        //            amount = _trades[hour][resource];
        //    }

        //    return amount;
        //}

        //public void UpdateTrades(TradeUnit<float>[] trades, States state)
        //{
        //    if (trades == null) return;

        //    var multiplier = (state == States.on) ? 1 : -1;
        //    foreach (TradeUnit<float> unit in trades)
        //        UpdateTradesForHour(unit, multiplier);
        //}

        //private void UpdateTradesForHour(TradeUnit<float> unit, int multiplier)
        //{
        //    CheckTradesForHour(unit);
        //    _trades[unit.Hour][unit.Type] += multiplier * unit.Amount;
        //}

        //private void CheckTradesForHour(TradeUnit<float> unit)
        //{
        //    var hour = unit.Hour;
        //    var resource = unit.Type;

        //    if (!_trades.ContainsKey(unit.Hour))
        //        _trades.Add(unit.Hour, new Dictionary<Resource, float>());

        //    if (!_trades[hour].ContainsKey(resource))
        //        _trades[hour].Add(resource, 0f);
        //}


    }
}
