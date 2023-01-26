namespace LittleMars.Common.Signals
{
    public struct GoalIsDoneSignal
    {
        public ResultType ResultType;
        public int Index;
        public bool IsFirstDone;
    }
}
