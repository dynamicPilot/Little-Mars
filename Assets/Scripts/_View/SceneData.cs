using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LittleMars.View
{
    /// <summary>
    /// MonoBehaviout Data class to keep data for spawners.
    /// </summary>
    public class SceneData : MonoBehaviour
    {
        [Header("Slot Data")]
        public Transform SlotParent;
        public GameObject SlotPrefab;
    }
}
