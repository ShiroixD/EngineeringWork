using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestWorldController : MonoBehaviour, ISceneController
{
    public PlayerHelper PlayerHelper;
    public GameObject Congratulations;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InitializeScene()
    {

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
}
