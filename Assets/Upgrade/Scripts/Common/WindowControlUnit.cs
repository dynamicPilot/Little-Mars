using LittleMars.UI.Windows;
using LittleMars.WindowManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
