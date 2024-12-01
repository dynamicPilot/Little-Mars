using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemIcons
{
    [SerializeField] private Sprite[] icons;
    public Sprite[] Icons { get { return icons; } }

    [SerializeField] private RuntimeAnimatorController[] animator;
    public RuntimeAnimatorController[] Animator { get { return animator; } }
}
