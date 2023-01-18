namespace LittleMars.Model.Interfaces
{
    public interface IGoalTracker
    {
        bool Check();
    }

    public interface IOnGoalUpdated
    {
        void OnGoalUpdated();
    }

}
