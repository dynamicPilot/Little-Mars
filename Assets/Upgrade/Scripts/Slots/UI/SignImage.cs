using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LittleMars.Slots.UI
{
    public class SignImage : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void Resource()
        {
            //Debug.Log("Set image to image slot");
        }

        public class Factory: PlaceholderFactory<SignImage>
        {

        }
    }
}
