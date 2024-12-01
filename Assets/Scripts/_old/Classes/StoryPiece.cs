using UnityEngine;

[System.Serializable]
public class StoryPiece
{
    [SerializeField]private string header;
    public string Header { get { return header; } }

    [SerializeField] [TextArea(1, 20)] private string description;
    public string Description { get { return description; } }

    [SerializeField] private Sprite icon;
    public Sprite Icon { get { return icon; } }

    [SerializeField] private RuntimeAnimatorController animatorController;
    public RuntimeAnimatorController AnimatorController { get { return animatorController; } }
}
