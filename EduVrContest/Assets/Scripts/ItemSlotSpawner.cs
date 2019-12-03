using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotSpawner : MonoBehaviour
{
    private GameObject _currentItem;
    private uint _currentItemIndex;
    public GameObject[] Items;

    void Start()
    {
        _currentItemIndex = 0;
        _currentItem = Instantiate(Items[_currentItemIndex], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, gameObject.transform);
        _currentItem.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    void Update()
    {
        
    }

    public bool NextItem()
    {
        _currentItemIndex++;
        if (_currentItemIndex < Items.Length)
        {
            _currentItem = Instantiate(Items[_currentItemIndex], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, gameObject.transform);
            _currentItem.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            return true;
        } else
        {
            return false;
        }
    }
}
