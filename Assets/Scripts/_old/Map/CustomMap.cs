using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Items/New Map")]
public class CustomMap : ScriptableObject
{
    [SerializeField] private int rowCounter;
    public int RowCounter { get { return rowCounter; } }

    [SerializeField] private int columnCounter;
    public int ColumnCounter { get { return columnCounter; } }

    [SerializeField] private MapUnitLine[] mapByLines;
    public MapUnitLine[] MapByLines { get { return mapByLines; } }
}
