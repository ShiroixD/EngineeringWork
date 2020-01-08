using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRoomController : MonoBehaviour, ISceneController
{
    public GameObject[] Stars;
    public GameObject GameInfo;
    public GameObject ObjectToHideWhenInfo;
    private GameManager _gameManager;
    private bool _showingInfo;

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

    public void ShowGameInfo()
    {
        if (!_showingInfo)
        {
            _showingInfo = true;
            Vector3 pos = ObjectToHideWhenInfo.transform.position;
            ObjectToHideWhenInfo.transform.position = new Vector3(pos.x, pos.y - 10.0f, pos.z);
            GameInfo.SetActive(true);
        }
    }

    public void HideGameInfo()
    {
        _showingInfo = false;
        Vector3 pos = ObjectToHideWhenInfo.transform.position;
        ObjectToHideWhenInfo.transform.position = new Vector3(pos.x, pos.y + 10.0f, pos.z);
        GameInfo.SetActive(false);
    }
}
