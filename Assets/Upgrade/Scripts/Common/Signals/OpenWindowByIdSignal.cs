using LittleMars.WindowManagers;

namespace LittleMars.Common.Signals
{
    public struct OpenWindowByIdSignal
    {
        public int Id;
        public int SenderId;
        public int NextSenderState;
        public WindowContext Context;
    }

    public struct WindowStateByIdSignal
    {
        public int Id;
        public int SenderState;
    }

}
