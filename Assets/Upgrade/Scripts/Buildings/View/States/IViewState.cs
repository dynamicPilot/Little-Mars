using System;

namespace LittleMars.Buildings.View.States
{
    public interface IViewState : IDisposable
    {
        void SetView();
        void CloseView();
    }
}