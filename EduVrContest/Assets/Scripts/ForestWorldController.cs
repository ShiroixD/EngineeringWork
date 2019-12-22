using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestWorldController : MonoBehaviour, ISceneController
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartScene()
    {

    }

    public void RestartScene()
    {

    }

    public void EndScene()
    {
        
    }

    public void CleanUp()
    {
        GameObject[] sceneGameObjects = gameObject.scene.GetRootGameObjects();
        foreach (GameObject obj in sceneGameObjects)
        {
            obj.SetActive(false);
        }
    }
}
