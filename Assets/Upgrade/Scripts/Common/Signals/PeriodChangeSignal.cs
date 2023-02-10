namespace LittleMars.Common.Signals
{
    public struct PeriodChangeSignal
    {
        public Period Period { get; set; }
    }

    public struct HourlySignal
    {
        public int Hour { get; set; }
    }
    public struct TimeSpeedChangedSignal
    {
    }
}
