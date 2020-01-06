using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, string> _gameSceneNameMap;
    private Dictionary<string, bool> _completedWorlds;
    private string _currentGameName;
    private SubSceneManager _subSceneManager;

    void Awake()
    {
        _gameSceneNameMap = new Dictionary<string, string>();
        _completedWorlds = new Dictionary<string, bool>();
        _gameSceneNameMap.Add("ControlRoom", "ControlRoomScene");
        _gameSceneNameMap.Add("Forest", "ForestScene");
        _gameSceneNameMap.Add("Tavern", "TavernScene");
        _gameSceneNameMap.Add("School", "SchoolScene");
        _completedWorlds.Add("Forest", false);
        _completedWorlds.Add("Tavern", false);
        _completedWorlds.Add("School", false);
    }
    void Start()
    {
        _subSceneManager = GetComponent<SubSceneManager>();
        _currentGameName = "ControlRoom";
        LoadGame(_currentGameName);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            LoadGame("ControlRoom");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ReloadGame();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            LoadGame("Forest");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            LoadGame("Tavern");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame("School");
        }
    }

    public void LoadGame(string gameName)
    {
        if (_gameSceneNameMap.ContainsKey(gameName) && !_subSceneManager.ChangingScene)
        {
            _currentGameName = gameName;
            _subSceneManager.LoadSubScene(_gameSceneNameMap[_currentGameName]);
        }
    }

    public void ReloadGame()
    {
        if (!_subSceneManager.ChangingScene)
        {
            _subSceneManager.LoadSubScene(_gameSceneNameMap[_currentGameName]);
        }
    }

    public void ShowInfo()
    {

    }

    public void CompletedWorld(string name)
    {
        _completedWorlds[name] = true;
    }

    public bool CheckIfWorldCompleted(string name)
    {
        return _completedWorlds[name];
    }
}
