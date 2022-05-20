using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryClassForDescription
{
    [SerializeField] private GameMaster.LANG lang;
    public GameMaster.LANG Lang { get { return lang; } }

    [TextArea] [SerializeField] private string description;
    public string Description { get { return description; } }
}
