using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRoomController : MonoBehaviour, ISceneController
{
    public GameObject[] Stars;
    private GameManager _gameManager;

    void Start()
    {
        InitializeScene();
    }

    void Update()
    {
        
    }

    public void InitializeScene()
    {
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (_gameManager != null)
        {
            if (_gameManager.CheckIfWorldCompleted("Forest"))
            {
                Stars[0].SetActive(true);
            }
            if (_gameManager.CheckIfWorldCompleted("Tavern"))
            {
                Stars[1].SetActive(true);
            }
            if (_gameManager.CheckIfWorldCompleted("School"))
            {
                Stars[2].SetActive(true);
            }
        }
    }

    public void FinishScene()
    {

    }
}
