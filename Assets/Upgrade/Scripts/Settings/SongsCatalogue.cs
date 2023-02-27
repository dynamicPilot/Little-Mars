using UnityEngine;

namespace LittleMars.Settings
{
    [CreateAssetMenu(menuName = "LittleMars/Catalogue/SongsSettings")]
    public class SongsCatalogue : ScriptableObject
    {
        public AudioClip[] Songs;
    }
}
