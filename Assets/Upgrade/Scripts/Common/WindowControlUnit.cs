using LittleMars.WindowManagers;
using System;
using UnityEngine.UI;

namespace LittleMars.Common
{
    [Serializable]
    public class WindowControlUnit<T>
    {
        public T Unit;
        public WindowID WindowID;
    }

    [Serializable]
    public class WindowStateControlUnit<T>
    {
        public T Unit;
        public WindowState SenderState;
    }

    [Serializable]
    public class WindowControlWithStateUnit : WindowControlUnit<Button>
    {
        public WindowState SenderState;
    }

    [Serializable]
    public class CommandSenderUnit<T>
    {
        public T Unit;
        public CommandType CommandType;
    }

    //[Serializable]
    //public class ButtonOpenWindowUnit
    //{
    //    public Button Button;
    //    public WindowID WindowID;
    //}
}
