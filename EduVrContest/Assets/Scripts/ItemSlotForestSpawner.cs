using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotForestSpawner : MonoBehaviour
{
    private GameObject _currentItem;
    private List<uint> usedItemsIndexes;
    private System.Random rnd = new System.Random();
    public GameObject[] Items;
    public Vector3 ItemScale;
    public GameObject Effect;

    void Start()
    {
        usedItemsIndexes = new List<uint>();
        uint index = (uint)rnd.Next(Items.Length);
        usedItemsIndexes.Add(index);
        _currentItem = Instantiate(Items[index], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, gameObject.transform);
        _currentItem.transform.localScale = ItemScale;
        ShowEffect();
    }

    void Update()
    {
        
    }

    IEnumerator EffectDelay()
    {
        Effect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Effect.SetActive(false);
    }

    public void ShowEffect()
    {
        StartCoroutine("EffectDelay");
    }

    public bool SpawnNextItem()
    {
        if (usedItemsIndexes.Count < Items.Length)
        {
            ShowEffect();
            uint number = (uint)rnd.Next(Items.Length);
            while (usedItemsIndexes.Contains(number))
            {
                number = (uint)rnd.Next(Items.Length);
            }
            usedItemsIndexes.Add(number);
            _currentItem = Instantiate(Items[number], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, gameObject.transform);
            _currentItem.transform.localScale = ItemScale;
            return true;
        } else
        {
            return false;
        }
    }
}
