using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

[RequireComponent(typeof(InteractionBehaviour))]
public class FigureItem : MonoBehaviour
{
    private Vector3 _itemSpawnLocation;
    private ItemSlotForestSpawner _itemSlotSpawner;
    private InteractionBehaviour _intObj;
    public FigureName FigureName;

    void Start()
    {
        GameObject itemSpawner = GameObject.FindWithTag("ItemSpawn");
        _itemSpawnLocation = itemSpawner.transform.position;
        _itemSlotSpawner = itemSpawner.GetComponent<ItemSlotForestSpawner>();
        _intObj = GetComponent<InteractionBehaviour>();
    }

    void Update()
    {
        
    }

    void Disappear()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            _intObj.ignoreGrasping = true;
            _intObj.ignoreContact = true;
            _intObj.ignorePrimaryHover = true;
            transform.position = _itemSpawnLocation;
            _itemSlotSpawner.ShowEffect();
            _intObj.ignoreGrasping = false;
            _intObj.ignoreContact = false;
            _intObj.ignorePrimaryHover = false;
            return;
        }

        if (collision.collider.tag == "FigureSlot" && 
            gameObject.GetComponent<FigureItem>().FigureName.Equals(
                collision.collider.GetComponent<FigureSlot>().FigureName
            )
        )
        {
            _intObj.ignoreGrasping = true;
            _intObj.ignoreContact = true;
            _intObj.ignorePrimaryHover = true;
            Disappear();
        }
    }
}
