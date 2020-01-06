using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, string> _gameSceneNameMap;
    private string _currentGameName;
    private SubSceneManager _subSceneManager;

    void Awake()
    {
        _gameSceneNameMap = new Dictionary<string, string>();
        _gameSceneNameMap.Add("ControlRoom", "ControlRoomScene");
        _gameSceneNameMap.Add("Forest", "ForestScene");
        _gameSceneNameMap.Add("Tavern", "TavernScene");
        _gameSceneNameMap.Add("School", "SchoolScene");
    }
    void Start()
    {
        _subSceneManager = GetComponent<SubSceneManager>();
        _currentGameName = "ControlRoom";
        LoadGame(_currentGameName);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            LoadGame("ControlRoom");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ReloadGame();
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
}
