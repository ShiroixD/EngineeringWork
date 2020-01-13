using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestWorldController : MonoBehaviour, ISceneController
{
    public PlayerHelper PlayerHelper;
    public GameObject Congratulations;
    public GameObject GameInfo;
    public GameObject[] Objects;
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
        _showingInfo = false;
    }

    public void FinishScene()
    {
        Congratulations.SetActive(true);
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CompletedWorld("Forest");
        StartCoroutine(DelayedReturn(5.0f));
    }

    IEnumerator DelayedReturn(float sec)
    {
        yield return new WaitForSeconds(sec);
        PlayerHelper.ReturnToControlRoom();
    }

    public void ShowGameInfo()
    {
        if (!_showingInfo)
        {
            _showingInfo = true;
            Objects[0] = GameObject.FindWithTag("Item");
            for (int i = 0; i < Objects.Length; i++)
            {
                Vector3 pos = Objects[i].transform.position;
                Objects[i].transform.position = new Vector3(pos.x, pos.y - 10.0f, pos.z);
            }
            GameInfo.SetActive(true);
        }  
    }

    public void HideGameInfo()
    {
        _showingInfo = false;
        for (int i = 0; i < Objects.Length; i++)
        {
            Vector3 pos = Objects[i].transform.position;
            Objects[i].transform.position = new Vector3(pos.x, pos.y + 10.0f, pos.z);
        }
        GameInfo.SetActive(false);
    }
}
