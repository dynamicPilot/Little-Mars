namespace LittleMars.Common
{
    public enum OperationMode
    {
        auto,
        manual,
        forcedAuto
    }

    public enum States
    {
        on,
        off
    }

    public enum BStates
    {
        on, off, paused
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

    public enum GoalType
    {
        resources,
        production,
        building,
        time
    }

    public enum ResultType
    {
        win,
        lose
    }

    public enum IconType
    {
        resource,
        building,
        goalType,
        field,
        timer
    }

    public enum MenuInitType
    {
        resources,
        goals
    }

    public enum Identifiers
    {
        buildingSlot,
        fieldSlot,
        connectionSlot
    }

    public enum MenuMode
    {
        start,
        end
    }

    public enum CommandType
    {
        next,
        mainMenu,
        restart,
        start,
        menu,
        back,
        muteMusic,
        muteAll,
        empty
    }

    public enum ColorType
    {
        connection,
        bState
    }

    public enum ScreenType
    {
        buildingCost,
        production,
        needs
    }

    public enum Langs
    {
        en,
        ru
    }

    public enum TagGroup
    {
        scene,
        level
    }
}

