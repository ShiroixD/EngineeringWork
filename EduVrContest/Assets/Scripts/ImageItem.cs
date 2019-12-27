using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageItem
{
    public Sprite image;
    public GameObject item;

    public ImageItem(Sprite img, GameObject obj)
    {
        image = img;
        item = obj;
    }
}
