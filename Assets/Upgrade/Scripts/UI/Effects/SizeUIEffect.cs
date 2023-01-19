using UnityEngine;


namespace LittleMars.UI.Effects
{
    public class SizeUIEffect : MonoBehaviour
    {
        [SerializeField] private GameObject[] _signs;

        public void SetSize(int index)
        {
            for (int i = 0; i < _signs.Length; i++)
            {
                _signs[i].SetActive(i <= index);
            }
        }
    }
}

