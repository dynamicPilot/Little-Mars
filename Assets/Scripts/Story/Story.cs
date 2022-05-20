using UnityEngine;

[CreateAssetMenu(fileName = "New Story", menuName = "Items/New Story")]
public class Story : ScriptableObject
{
    [SerializeField] private GameMaster.LANG lang;
    public GameMaster.LANG Lang { get { return lang; } }

    [SerializeField] private StoryPiece[] pieces;
    public StoryPiece[] Pieces { get { return pieces; } }
}
