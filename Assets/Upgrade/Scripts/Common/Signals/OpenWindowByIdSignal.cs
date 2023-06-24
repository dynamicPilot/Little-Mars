namespace LittleMars.Common.Signals
{
    public struct OpenWindowByIdSignal
    {
        public int Id;
        public int SenderId;
        public int NextSenderState;
    }

    public struct WindowStateByIdSignal
    {
        public int Id;
        public int SenderState;
    }

}
