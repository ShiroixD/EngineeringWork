using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

[RequireComponent(typeof(InteractionBehaviour))]
public class FigureItem : MonoBehaviour
{
    private Vector3 _itemSpawnLocation;
    private InteractionBehaviour _intObj;
    public FigureName FigureName;

    void Start()
    {
        _itemSpawnLocation = GameObject.FindWithTag("ItemSpawn").transform.position;
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
            transform.position = _itemSpawnLocation;
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
