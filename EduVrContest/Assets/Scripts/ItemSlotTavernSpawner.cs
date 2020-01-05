using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotTavernSpawner : MonoBehaviour
{
    public GameObject EffectAnchor1;
    public GameObject EffectAnchor2;
    public GameObject VegetablesAnchor;
    public GameObject FruitsAnchor;

    private System.Random rnd = new System.Random();
    private List<GameObject> SpawnedItems;

    private void Awake()
    {
        SpawnedItems = new List<GameObject>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SpawnItem(int anchorNumber, GameObject obj)
    {
        GameObject spawnedObj = null;
        if (anchorNumber == 1)
        {
            spawnedObj = Instantiate(obj, new Vector3(
                VegetablesAnchor.transform.position.x,
                VegetablesAnchor.transform.position.y,
                VegetablesAnchor.transform.position.z),
                Quaternion.identity);
        }
        else if (anchorNumber == 2)
        {
            spawnedObj = Instantiate(obj, new Vector3(
                FruitsAnchor.transform.position.x,
                FruitsAnchor.transform.position.y,
                FruitsAnchor.transform.position.z),
                Quaternion.identity);
        }
    }
}
