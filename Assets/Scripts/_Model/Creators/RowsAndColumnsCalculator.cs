namespace LittleMars.Models.Creators
{
    public class RowsAndColumnsCalculator : IRowsAndColumnsCalculator
    {
        LevelInfo _levelInfo;

        public RowsAndColumnsCalculator(LevelInfo levelInfo)
        {
            _levelInfo = levelInfo;
        }

        public void CalculateRowsAndColumns(out int rows, out int columns)
        {
            rows = (_levelInfo.AutoMap) ? _levelInfo.RowCounter : _levelInfo.CustomeAutoMap.RowCounter;
            columns = (_levelInfo.AutoMap) ? _levelInfo.ColumnCounter : _levelInfo.CustomeAutoMap.ColumnCounter;
        }
    }

}
