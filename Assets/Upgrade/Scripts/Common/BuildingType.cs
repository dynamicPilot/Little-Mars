namespace LittleMars.Common
{
    public enum OperationMode
    {
        auto,
        manual,
        forcedAuto
    }

    public enum ProductionState
    {
        on,
        off
    }

    public enum BuildingType
    {
        dome,
        power_plant,
        mine,
        farm,
        factory,
        supply_plant,
        workshop,
        cosmodrome,
        all,
        none
    }

    public enum Size
    {
        small,
        medium,
        large
    }

    public enum Priority
    {
        low,
        medium,
        high,
        ultimate
    }

    public enum Resource
    {
        money,
        metalls,
        food,
        machines,
        energy,
        goods,
        supply_units,
        all,
        none
    }

    public enum Period
    {
        day,
        night
    }

    public enum Direction
    {
        left,
        up,
        right,
        down
    }
}

