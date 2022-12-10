using System.Collections.Generic;

namespace LittleMars.Models.Common
{
    public class GridUnit
    {
        public bool IsBlocked { get; set; }
        public List<Inventory.R_TYPE> Resources { get; private set; }
        public List<Inventory.B_TYPE> Buildings { get; private set; }
        public GridUnit[] Neighbors { get; private set; }

        public GridUnit(bool isBlocked)
        {
            IsBlocked = isBlocked;
            
            Resources = new List<Inventory.R_TYPE>();
            Buildings = new List<Inventory.B_TYPE>();
            Neighbors = null;
        }

        public GridUnit(bool isBlocked, List<Inventory.B_TYPE> buildings)
        {
            IsBlocked = isBlocked;
            Resources = new List<Inventory.R_TYPE>();
            Buildings = buildings;
            Neighbors = null;
        }

        public void SetNeighbors(GridUnit[] neighbors)
        {
            Neighbors = neighbors;
        }

        public void AddBuildingType(Inventory.B_TYPE type)
        {
            if (!Buildings.Contains(type))
                Buildings.Add(type);
        }

        public void AddResourceType(Inventory.R_TYPE type)
        {
            if (!Resources.Contains(type))
                Resources.Add(type);
        }
    }
}

