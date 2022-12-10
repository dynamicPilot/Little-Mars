using LittleMars.Models.Common;
using System.Collections.Generic;

namespace LittleMars.Models.Creators
{
    public class FieldsCreator : IGridUnitsFieldsCreator
    {
        int _maxInterationNumber;
        MapField[] _fields;

        public FieldsCreator(LevelInfo levelInfo)
        {
            _maxInterationNumber = 10;
            _fields = levelInfo.Fields;
        }

        public void CreateGridUnitsFields(List<List<GridUnit>> units)
        {
            return;
        }
    }

}
