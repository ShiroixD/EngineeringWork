using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    SubSceneManager _subSceneManager;
    private bool _loadedMainScene = false;
    void Start()
    {
        _subSceneManager = GetComponent<SubSceneManager>();
    }

    void Update()
    {
        if (!_loadedMainScene)
        {
            _subSceneManager.LoadSubScene("ForestScene");
            _loadedMainScene = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _subSceneManager.LoadSubScene("ForestScene");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            _subSceneManager.UnloadCurrentSubScene();
        }
    }
}
