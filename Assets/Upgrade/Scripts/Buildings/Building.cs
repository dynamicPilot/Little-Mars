using LittleMars.Buildings.Parts;
using LittleMars.Buildings.States;
using LittleMars.Buildings.View;
using LittleMars.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleMars.Buildings
{
    public class Building
    {
        private List<BuildingFacade> _neighbors;

        //public Building(BuildingView.Factory factory)
        //{
        //    factory.Create();
        //}

        public bool HasAllConnections()
        {
            return true;
        }
    }
}
