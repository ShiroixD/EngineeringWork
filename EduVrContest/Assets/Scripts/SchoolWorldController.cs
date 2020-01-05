using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolWorldController : MonoBehaviour, ISceneController
{
    public PlayerHelper PlayerHelper;
    private System.Random rnd = new System.Random();

    void Start()
    {
        InitializeScene();
    }

    void Update()
    {
        
    }

    public void InitializeScene()
    {

    }

    public void FinishScene()
    {
        PlayerHelper.ReturnToControlRoom();
    }
}
