using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesCubeController : MonoBehaviour
{
    public ItemSlotSpawner ItemSlotSpawner;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DecreaseShapesCubeParts()
    {
        if (!ItemSlotSpawner.NextItem())
        {
            gameObject.SetActive(false);
        }
    }

}
