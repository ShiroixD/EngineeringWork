using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Animation;

public class PlayerHelper : MonoBehaviour
{
    public GameObject[] HandsAnchorsObjects;
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        StartCoroutine(DelayedLeapHandsSetup());
    }

    void Update()
    {
        
    }

    private IEnumerator DelayedLeapHandsSetup()
    {
        yield return new WaitForSeconds(0.2f);
        foreach(GameObject obj in HandsAnchorsObjects)
        {
            obj.SetActive(false);
        }
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
        GameObject.FindGameObjectWithTag("GameController").GetComponent<ISceneController>().ShowGameInfo();
    }

    public void GoToForest()
    {
        _gameManager.LoadGame("Forest");
    }

    public void GoToTavern()
    {
        _gameManager.LoadGame("Tavern");
    }

    public void GoToSchool()
    {
        _gameManager.LoadGame("School");
    }
}
