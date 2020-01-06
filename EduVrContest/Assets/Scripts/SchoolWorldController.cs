using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchoolWorldController : MonoBehaviour, ISceneController
{
    public PlayerHelper PlayerHelper;
    public string[] IconsNames;
    public Sprite[] IconsSprites;
    public GameObject[] Bars;
    public GameObject[] Buttons;
    public SpriteRenderer TaskImage;
    private int TASKS_LIMIT = 10;
    private int ANSWERS_AMOUNT = 3;
    private System.Random _rnd = new System.Random();
    private int _score;
    private int _currentTaskId;
    private int _currentCorrectAnswear;
    private List<int> _usedTasksIds;
    private const int BUTTON_DEFAULT_COLOR = 0xd0d0d0;
    private const int BUTTON_GREEN_COLOR = 0x6ef864;
    private const int BUTTON_RED_COLOR = 0xff675d;

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

    public Color hexToRgb(int number)
    {
        var r = (number >> 16) & 255;
        var g = (number >> 8) & 255;
        var b = number & 255;

        return new Color(r, g, b);
    }

    public void GiveTask()
    {
        if (_usedTasksIds.Count >= IconsNames.Length)
        {
            _usedTasksIds.Clear();
        }
        int number;
        do
        {
            number = _rnd.Next(0, IconsNames.Length);
        } while (_usedTasksIds.Contains(number));
        _currentTaskId = number;
        _currentCorrectAnswear = _rnd.Next(0, ANSWERS_AMOUNT);
        string[] availableAnswers = new string[ANSWERS_AMOUNT];
        availableAnswers[_currentCorrectAnswear] = IconsNames[_currentTaskId];
        ArrayList otherAnswers = new ArrayList();
        do
        {
            number = _rnd.Next(0, IconsNames.Length);
            if (!otherAnswers.Contains(number) && number != _currentTaskId)
            {
                otherAnswers.Add(number);
            }
        } while (otherAnswers.Count < (ANSWERS_AMOUNT - 1));
        number = 0;
        for (int i = 0; i < availableAnswers.Length; i++)
        {
            if (availableAnswers[i] == null)
            {
                availableAnswers[i] = IconsNames[(int)otherAnswers[number]];
                number++;
            }
        }
        TaskImage.sprite = IconsSprites[_currentTaskId];
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].transform.Find("Answer").gameObject.
                GetComponent<TextMesh>().text = availableAnswers[i];
        }
    }

    public void GiveAnswer(int number)
    {
        GameObject button = Buttons[number];
        if (number == _currentCorrectAnswear)
        {
            Bars[_score].SetActive(true);
            _score++;
            button.transform.Find("BackCube").
                gameObject.transform.Find("FrontCube").
                gameObject.GetComponent<Renderer>().material.color = hexToRgb(BUTTON_GREEN_COLOR);
        }
        else
        {

        }
    }
}
