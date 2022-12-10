using System.Collections;
using System.Collections.Generic;


namespace LittleMars.Common
{

    [System.Serializable]
    public class ResourceByPeriodsUnit
    {
        public Resource Resource { get; private set; }
        public Dictionary <Period, float> Amounts { get; private set; }
        public ResourceByPeriodsUnit(Resource resource)
        {
            Resource = resource;
            Amounts = new Dictionary<Period, float>();
        }

        public void UpdateAmountForPeriod(Period period, float amount)
        {
            Amounts[period] = amount;
        }

    }
}

