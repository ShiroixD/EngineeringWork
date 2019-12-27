using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageItem
{
    public Sprite image;
    public GameObject item;
    public string itemType;

    public ImageItem(Sprite img, GameObject obj, string type)
    {
        image = img;
        item = obj;
        itemType = type;
    }
}
