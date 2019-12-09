using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguresContainerController : MonoBehaviour
{
    public ItemSlotSpawner ItemSlotSpawner;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DecreaseFiguresSlots()
    {
        if (!ItemSlotSpawner.SpawnNextItem())
        {
            gameObject.SetActive(false);
        }
    }

}
