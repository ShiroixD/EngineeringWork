using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Leap.Unity.Interaction;

public class SchoolWorldController : MonoBehaviour, ISceneController
{
    public PlayerHelper PlayerHelper;
    public string[] IconsNames;
    public Sprite[] IconsSprites;
    public GameObject[] Bars;
    public GameObject[] Buttons;
    public SpriteRenderer TaskImage;
    public TextMeshPro CorrectAnswerText;
    public GameObject TaskLabel;
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

        if (Input.GetKeyDown(KeyCode.I))
        {
            Bars[_score].SetActive(true);
            _score++;
            if (_score >= TASKS_LIMIT)
            {
                FinishScene();
            }
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
        TaskLabel.SetActive(false);
        Buttons[0].transform.parent.gameObject.SetActive(false);
        TaskImage.gameObject.SetActive(false);
        CorrectAnswerText.text = "";
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().CompletedWorld("School");
        PlayerHelper.ReturnToControlRoom();
    }

    public Color HexToColor(int number)
    {
        int r = (number >> 16) & 255;
        int g = (number >> 8) & 255;
        int b = number & 255;
        return new Color(r / 255.0f, g / 255.0f, b / 255.0f);
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
        CorrectAnswerText.text = "";
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].transform.Find("BackCube").
                gameObject.transform.Find("FrontCube").
                gameObject.transform.Find("Answer").
                gameObject.GetComponent<TextMeshPro>().text = availableAnswers[i];
        }
    }

    public void GiveAnswer(int number)
    {
        StartCoroutine(CheckAnswer(number));
    }

    IEnumerator CheckAnswer(int number)
    {
        BlockButtons();
        yield return new WaitForSeconds(2.0f);
        CorrectAnswerText.text = IconsNames[_currentTaskId];
        GameObject button = Buttons[number];
        if (number == _currentCorrectAnswear)
        {
            Bars[_score].SetActive(true);
            _score++;
            button.transform.Find("BackCube").
                gameObject.transform.Find("FrontCube").
                gameObject.GetComponent<Renderer>().material.color = HexToColor(BUTTON_GREEN_COLOR);
            yield return new WaitForSeconds(4.0f);
            button.transform.Find("BackCube").
                gameObject.transform.Find("FrontCube").
                gameObject.GetComponent<Renderer>().material.color = HexToColor(BUTTON_DEFAULT_COLOR);
            if (_score >= TASKS_LIMIT)
            {
                FinishScene();
            }
            else
            {
                GiveTask();
            }
        }
        else
        {
            button.transform.Find("BackCube").
                gameObject.transform.Find("FrontCube").
                gameObject.GetComponent<Renderer>().material.color = HexToColor(BUTTON_RED_COLOR);
            yield return new WaitForSeconds(3.0f);
            button.transform.Find("BackCube").
                gameObject.transform.Find("FrontCube").
                gameObject.GetComponent<Renderer>().material.color = HexToColor(BUTTON_DEFAULT_COLOR);
            GiveTask();
        }
        UnlockButtons();
    }

    private void BlockButtons()
    {
        foreach(GameObject btn in Buttons)
        {
            InteractionBehaviour behaviour = btn.GetComponent<InteractionBehaviour>();
            behaviour.ignoreContact = true;
            behaviour.ignorePrimaryHover = true;
        }
    }

    private void UnlockButtons()
    {
        foreach (GameObject btn in Buttons)
        {
            InteractionBehaviour behaviour = btn.GetComponent<InteractionBehaviour>();
            behaviour.ignoreContact = false;
            behaviour.ignorePrimaryHover = false;
        }
    }
}
