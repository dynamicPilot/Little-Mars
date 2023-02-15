namespace LittleMars.PlayerStates
{
    public interface IPlayerState
    {
        int GetLevelNumber();
        void ToNextLevel();
    }
}
