using UnityEngine;

namespace LittleMars.Connections.View
{
    public class ConnectionIndicator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        public void SetIndicator(Color color)
        {
            _renderer.color = color;
        }

    }
}
