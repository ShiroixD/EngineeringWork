﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestWorldController : MonoBehaviour, ISceneController
{
    public PlayerHelper PlayerHelper;

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
        PlayerHelper.ReturnToControlRoom();
    }
}
