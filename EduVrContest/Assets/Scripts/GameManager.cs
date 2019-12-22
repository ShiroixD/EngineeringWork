using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    SubSceneManager _subSceneManager;
    void Start()
    {
        _subSceneManager = GetComponent<SubSceneManager>();
    }

    void Update()
    {
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
