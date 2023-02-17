using UnityEngine;
using Zenject;

namespace LittleMars.Commands
{
    public class NextCommand : IExtendedCommand
    {
        Receiver _receiver;

        //public NextCommand(Receiver receiver)
        //{
        //    _receiver = receiver;
        //}

        public void Execute()
        {
            _receiver.Next();
        } 
        public void ChangeReceiver(Receiver receiver)
        {
            _receiver = receiver;
        } 

        public class Factory : PlaceholderFactory<NextCommand>
        { }
    }
}
