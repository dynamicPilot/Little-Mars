﻿namespace LittleMars.Common.Signals
{
    /// <summary>
    /// A signal for start level. Before the tutorial piece, after StartMenu closures.
    /// </summary>
    public struct StartLevelSignal
    { }

    /// <summary>
    /// A signal for the game start. After the tutorial part ending.
    /// </summary>
    public struct StartGameSignal
    { }

    public struct GameOverSignal
    {
        public int GoalIndex;
        public bool IsStaff;
    }

    /// <summary>
    /// A signal for the end game init. Then some delay would be performed for smooth player ending.
    /// </summary>
    public struct EndGameReachedSignal
    { }

    /// <summary>
    /// A signal for the game ending.
    /// </summary>
    public struct EndGameSignal
    { }

    /// <summary>
    /// A signal for the level end. Before the possible advertisement integration.
    /// </summary>
    public struct EndLevelSignal
    {
    }

    /// <summary>
    /// A signal that is called in the very end of the level process. After ads integration.
    /// </summary>
    public struct EndSceneSignal
    { }


    public struct ToLevelSignal
    {
        public int Index;
    }
}
