using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string _currentGameSceneName;
    SubSceneManager _subSceneManager;

    void Start()
    {
        _subSceneManager = GetComponent<SubSceneManager>();
        _currentGameSceneName = "ForestScene";
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _subSceneManager.LoadSubScene("ForestScene");
        }
    }

    public void ReloadGame()
    {
        _subSceneManager.LoadSubScene(_currentGameSceneName);
    }
}
