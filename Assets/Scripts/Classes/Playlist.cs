using UnityEngine;

[System.Serializable]
public class Playlist 
{
    [SerializeField] string title;
    public string Title { get { return title; } }

    [SerializeField] private Sound[] songs;
    public Sound[] Songs { get { return songs; } }
}
