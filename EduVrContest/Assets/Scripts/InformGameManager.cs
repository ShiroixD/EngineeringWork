using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformGameManager : MonoBehaviour
{
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        
    }

    public void RestartGame()
    {
        _gameManager.ReloadGame();
    }
}
