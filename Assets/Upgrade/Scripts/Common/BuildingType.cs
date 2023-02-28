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
        on, off, paused, effected
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
        large,
        extraLarge
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
        lose,
        loseStaff
    }

    public enum IconType
    {
        resource,
        building,
        goalType,
        field,
        timer,
        routeError
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
        mainMenuByStart,
        restart,
        start,
        back,
        muteMusic,
        muteAll,
        empty,
        quit,
        toLevel,
        goalsInfo
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

    public enum MenuState
    {
        start,
        tutorial,
        achievement,
        end,
        gameOver,
        none
    }

    public enum SceneType
    {
        menu,
        level
    }

    public enum UISoundType
    {
        clickFirst,
        clickSecond,
        clickThird,
        drop,
        destroy,
        turnOn,
        turnOff,
        quit,
        zoomIn,
        zoomOut
    }

    public enum NotSoundType
    {
        start,
        end,
        gameOver,
        canNotDo,
        achievement
    }

    public enum NotErrorType
    {
        resource,
        route
    }
}

