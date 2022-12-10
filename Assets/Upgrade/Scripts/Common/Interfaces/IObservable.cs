using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.Common.Interfaces
{
    public interface IObservable
    {
        void RegisterObserver(IObserver observer);
        void RemoveObservable(IObserver observer);

    }

    public interface IObserver
    {
        void Update();
    }

}
