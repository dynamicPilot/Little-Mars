using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicSlot : MonoBehaviour
{
    // item
    //[SerializeField] private BasicItem item;
    [SerializeField] private Image image;

    public virtual void SetImage(Sprite newImage, int rotationIndex = 0)
    {
        if (newImage != null)
        {
            //Debug.Log("BasicSlot: rotationIndex is " + rotationIndex);
            image.gameObject.SetActive(true);
            image.sprite = newImage;
            image.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 0);
            image.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, -90f*rotationIndex));

        }
        else
        {
            image.gameObject.SetActive(false);
        }
    }

    public virtual void RemoveImage()
    {
        image.gameObject.SetActive(false);
        image.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 0);
    }

}
