using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TavernWorldController : MonoBehaviour, ISceneController
{
    public const int FOOD_REQUIREMENTS_SLOTS = 4;
    public int FoodRequirementsMaxAmount;
    public PlayerHelper PlayerHelper;
    public ItemSlotTavernSpawner ItemSpawner;
    public Sprite[] VegetablesIcons;
    public Sprite[] FruitsIcons;
    public GameObject[] VegetablesItems;
    public GameObject[] FruitsItems;
    public Image[] FoodUiImages;
    public Text[] FoodUiTexts;
    public string[] FoodNames;
    public GameObject Congratulations;
    public GameObject GameInfo;
    public GameObject ObjectToHideWhenInfo;
    private System.Random rnd = new System.Random();
    private Dictionary<string, ImageItem> _iconsItemsDict;
    private FoodRequirement[] _foodRequirements;
    private int _currentFoodRequirementIndex;
    private FoodRequirementUI[] _uiElements;
    private int _currentRound;
    private bool _showingInfo;

    void Awake()
    {
        _iconsItemsDict = new Dictionary<string, ImageItem>();
    }

    void Start()
    {
        InitializeScene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            CreateItemQuery(1);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            CreateItemQuery(2);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            UpdateCurrentRequirement();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ResetRequirements();
        }
    }

    public void InitializeScene()
    {
        _currentRound = 1;
        _showingInfo = false;
        ParseFoodIconsItems();
        ParseFoodUiImagesTexts();
        CreateItemQuery(1);
    }

    public void FinishScene()
    {
        Congratulations.SetActive(true);
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CompletedWorld("Tavern");
        StartCoroutine(DelayedReturn(5.0f));
    }

    public void ShowGameInfo()
    {
        if (!_showingInfo)
        {
            _showingInfo = true;
            Vector3 pos = ObjectToHideWhenInfo.transform.position;
            ObjectToHideWhenInfo.transform.position = new Vector3(pos.x, pos.y - 10.0f, pos.z);
            List<GameObject> items = new List<GameObject>(GameObject.FindGameObjectsWithTag("Item"));
            foreach(GameObject obj in items)
            {
                Vector3 objPos = obj.transform.position;
                obj.transform.position = new Vector3(objPos.x, objPos.y - 10.0f, objPos.z);
            }
            GameInfo.SetActive(true);
        }
    }

    public void HideGameInfo()
    {
        _showingInfo = false;
        Vector3 pos = ObjectToHideWhenInfo.transform.position;
        ObjectToHideWhenInfo.transform.position = new Vector3(pos.x, pos.y + 10.0f, pos.z);
        List<GameObject> items = new List<GameObject>(GameObject.FindGameObjectsWithTag("Item"));
        foreach (GameObject obj in items)
        {
            Vector3 objPos = obj.transform.position;
            obj.transform.position = new Vector3(objPos.x, objPos.y + 10.0f, objPos.z);
        }
        GameInfo.SetActive(false);
    }

    IEnumerator DelayedReturn(float sec)
    {
        yield return new WaitForSeconds(sec);
        PlayerHelper.ReturnToControlRoom();
    }

    public void ParseFoodIconsItems()
    {
        FoodNames = new string[VegetablesItems.Length + FruitsItems.Length];
        if (VegetablesIcons.Length == VegetablesItems.Length)
        {
            int size = VegetablesItems.Length;
            for (int i = 0; i < size; i++)
            {
                FoodNames[i] = VegetablesItems[i].name;
                _iconsItemsDict.Add(VegetablesItems[i].name, new ImageItem(VegetablesIcons[i], VegetablesItems[i], "Vegetable"));
            }
        }
        if (FruitsIcons.Length == FruitsItems.Length)
        {
            int size = FruitsItems.Length;
            for (int i = 0; i < size; i++)
            {
                FoodNames[VegetablesIcons.Length + i] = FruitsItems[i].name;
                _iconsItemsDict.Add(FruitsItems[i].name, new ImageItem(FruitsIcons[i], FruitsItems[i], "Fruit"));
            }
        }
    }

    public void ParseFoodUiImagesTexts()
    {
        _uiElements = new FoodRequirementUI[FOOD_REQUIREMENTS_SLOTS];
        for (int i = 0; i < FOOD_REQUIREMENTS_SLOTS; i++)
        {
            _uiElements[i] = new FoodRequirementUI(FoodUiImages[i], FoodUiTexts[i]);
        }
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

    public void CreateItemQuery(int option)
    {
        _foodRequirements = new FoodRequirement[FOOD_REQUIREMENTS_SLOTS];
        _currentFoodRequirementIndex = 0;
        List<string> usedNames = new List<string>();
        for (int i = 0; i < FOOD_REQUIREMENTS_SLOTS; i++)
        {
            string nameValue = "";
            int amount = rnd.Next(1, FoodRequirementsMaxAmount);

            if (option == 1)
            {
                do
                {
                    nameValue = FoodNames[rnd.Next(0, VegetablesItems.Length)];
                } while (usedNames.Contains(nameValue));
            }
            else if (option == 2)
            {
                do
                {
                    nameValue = FoodNames[VegetablesItems.Length + rnd.Next(0, FruitsItems.Length)];
                } while (usedNames.Contains(nameValue));
            }
            else
            {
                return;
            }
            _foodRequirements[i] = new FoodRequirement(nameValue, amount);
            usedNames.Add(nameValue);
            _uiElements[i].image.sprite = _iconsItemsDict[nameValue].image;
            _uiElements[i].text.text = "x " + amount.ToString();

            foreach (FoodRequirementUI reqUI in _uiElements)
            {
                reqUI.image.enabled = true;
                reqUI.text.enabled = true;
            }
        }
    }

    public FoodRequirement GetCurrentRequirement()
    {
        return _foodRequirements[_currentFoodRequirementIndex];
    }

    public void UpdateCurrentRequirement()
    {
        if (_currentRound < 3)
        {
            Debug.Log("Round: " + _currentRound.ToString());
            FoodRequirement currentFoodRequirement = _foodRequirements[_currentFoodRequirementIndex];
            currentFoodRequirement.currentAmount += 1;
            _uiElements[_currentFoodRequirementIndex].text.text = "x " +
                (currentFoodRequirement.requiredAmount - currentFoodRequirement.currentAmount).ToString();
            if (currentFoodRequirement.currentAmount >= currentFoodRequirement.requiredAmount)
            {
                _uiElements[_currentFoodRequirementIndex].image
                    .transform.parent.gameObject.SetActive(false);
                _currentFoodRequirementIndex++;
            }
            if (_currentFoodRequirementIndex >= FOOD_REQUIREMENTS_SLOTS)
            {
                if (_currentRound == 1)
                {
                    _currentFoodRequirementIndex = 0;
                    _currentRound = 2;
                    ResetRequirements();
                    CreateItemQuery(_currentRound);
                }
                else if (_currentRound == 2)
                {
                    _currentRound = 3;
                    FinishScene();
                }
            }
        }
    }

    public void ResetRequirements()
    {
        for (int i = 0; i < _foodRequirements.Length; i++)
        {
            FoodRequirement req = _foodRequirements[i];
            req.currentAmount = 0;
            _uiElements[i].text.text = "x " + req.requiredAmount.ToString();
        }
        foreach(FoodRequirementUI reqUI in _uiElements)
        {
            reqUI.image.transform.parent.gameObject.SetActive(true);
        }
        _currentFoodRequirementIndex = 0;
    }

    public void ClearRemainingFood()
    {
        Debug.Log("Czyszczenie start ...");
        List<GameObject> items = new List<GameObject>(GameObject.FindGameObjectsWithTag("Item"));
        foreach(GameObject item in items)
        {
            item.GetComponent<CauldronItem>().DisappearNow();
        }
        Debug.Log("Czyszczenie stop ...");
    }
}
