using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ISceneController
{
    void StartScene();
    void RestartScene();
    void EndScene();
    void CleanUp();
}
