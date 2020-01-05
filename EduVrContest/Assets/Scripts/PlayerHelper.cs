using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelper : MonoBehaviour
{
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        
    }

    public void ReturnToControlRoom()
    {
        _gameManager.LoadGame("ControlRoom");
    }

    public void RestartGame()
    {
        _gameManager.ReloadGame();
    }

    public void ShowInfoTips()
    {

    }

    public void GoToForest()
    {
        _gameManager.LoadGame("Forest");
    }

    public void GoToTavern()
    {
        _gameManager.LoadGame("Tavern");
    }

    public void GoToLibrary()
    {

    }
}
