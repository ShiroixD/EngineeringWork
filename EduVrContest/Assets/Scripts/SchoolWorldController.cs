using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolWorldController : MonoBehaviour, ISceneController
{
    public PlayerHelper PlayerHelper;
    public string[] IconsNames;
    public Sprite[] IconsSprites;
    public GameObject[] Bars;
    public GameObject[] Buttons;
    private uint TASKS_LIMIT = 10;
    private uint ANSWERS_AMOUNT = 3;
    private System.Random _rnd = new System.Random();
    private uint _score;
    private uint _currentTaskId;
    private uint _currentCorrectAnswear;
    private List<int> _usedTasksIds;

    void Start()
    {
        InitializeScene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            GiveTask();
        }
    }

    public void InitializeScene()
    {
        _score = 0;
        _usedTasksIds = new List<int>();
        GiveTask();
    }

    public void FinishScene()
    {
        PlayerHelper.ReturnToControlRoom();
    }

    public void GiveTask()
    {
        //wylosowac number id zadania
        //wylosowac pozostale odpowiedzi
        //wypelnic tesktu buttonow
    }
}
