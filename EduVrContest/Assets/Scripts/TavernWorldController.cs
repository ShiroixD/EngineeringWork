using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TavernWorldController : MonoBehaviour, ISceneController
{
    public PlayerHelper PlayerHelper;
    public ItemSlotTavernSpawner ItemSpawner;
    public Sprite[] VegetablesIcons;
    public Sprite[] FruitsIcons;
    public GameObject[] VegetablesItems;
    public GameObject[] FruitsItems;

    private Dictionary<string, ImageItem> _iconsItemsDict;

    void Awake()
    {
        _iconsItemsDict = new Dictionary<string, ImageItem>();
    }

    void Start()
    {
        if (VegetablesIcons.Length == VegetablesItems.Length)
        {
            int size = VegetablesItems.Length;
            for (int i = 0; i < size; i++)
            {
                _iconsItemsDict.Add(VegetablesItems[i].name, new ImageItem(VegetablesIcons[i], VegetablesItems[i], "Vegetable"));
            }
        }
        if (FruitsIcons.Length == FruitsItems.Length)
        {
            int size = FruitsItems.Length;
            for (int i = 0; i < size; i++)
            {
                _iconsItemsDict.Add(FruitsItems[i].name, new ImageItem(FruitsIcons[i], FruitsItems[i], "Fruit"));
            }
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    ItemSpawner.SpawnItem(1, _iconsItemsDict["Broccoli"].item);
        //}

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    ItemSpawner.SpawnItem(2, );
        //}
    }

    public void InitializeScene()
    {

    }

    public void FinishScene()
    {
        PlayerHelper.ReturnToControlRoom();
    }

    public void SpawnFood(string name)
    {
        if (_iconsItemsDict.ContainsKey(name))
        {
            if (_iconsItemsDict[name].itemType.Equals("Vegetable"))
            {
                ItemSpawner.SpawnItem(1, _iconsItemsDict[name].item);
            }
            else if (_iconsItemsDict[name].itemType.Equals("Fruit"))
            {
                ItemSpawner.SpawnItem(2, _iconsItemsDict[name].item);
            }
        }
        
    }
}
