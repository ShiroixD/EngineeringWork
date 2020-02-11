using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubSceneManager : MonoBehaviour
{
    private List<string> _scenesNames;
    private LoadSceneParameters parameters;
    private Scene? _currentSubScene;
    private GameObject _currentWorldController;
    private ISceneController _currentSubSceneController;
    public MusicManager MusicManager;
    public ScreenFadeEffect ScreenFadeEffect;
    public bool ChangingScene;

    void Awake()
    {
        _scenesNames = new List<string>();
        _currentSubScene = null;
        parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        ChangingScene = false;
        LoadScenesNames();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void LoadScenesNames()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            if (!sceneName.Equals("MainScene"))
            {
                _scenesNames.Add(sceneName);
            }
        }
    }

    private string ConvertSceneNameToAlias(string sceneName)
    {
        string result = "";
        switch(sceneName)
        {
            case "ControlRoomScene":
                {
                    result = "ControlRoom";
                    break;
                }
            case "ForestScene":
                {
                    result = "Forest";
                    break;
                }
            case "TavernScene":
                {
                    result = "Tavern";
                    break;
                }
            case "SchoolScene":
                {
                    result = "School";
                    break;
                }
            default:
                {
                    break;
                }
        }
        return result;
    }

    private IEnumerator LoadSubSceneAsync(string sceneName)
    {
        if (_currentSubScene != null)
        {
            ChangingScene = true;
            StartCoroutine(UnloadCurrentSubSceneAsync());
        }
        while(ChangingScene)
        {
            yield return null;
        }
        MusicManager.PlayMusic(ConvertSceneNameToAlias(sceneName));
        if (_scenesNames.Contains(sceneName))
        {
            ChangingScene = true;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, parameters);
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone)
            {
                if (!ScreenFadeEffect.AnimationIsPlaying && asyncOperation.progress >= 0.8f)
                {
                    ScreenFadeEffect.FadeInEffect();
                    asyncOperation.allowSceneActivation = true;
                }
                yield return null;
            }
            _currentSubScene = SceneManager.GetSceneByName(sceneName);
            GameObject sceneController = GameObject.FindGameObjectWithTag("GameController");
            GameObject physicsCallbacksProvider = GameObject.Find("Physics Callbacks Provider");
            if (physicsCallbacksProvider != null)
            {
                physicsCallbacksProvider.transform.parent = sceneController.transform;
            }
            SceneManager.SetActiveScene((Scene)_currentSubScene);
            ChangingScene = false;
            Debug.Log("Loaded subscene: " + ((Scene)_currentSubScene).name);
        }

        yield return null;
    }

    private IEnumerator UnloadCurrentSubSceneAsync()
    {
        if (_currentSubScene != null)
        {
            ScreenFadeEffect.FadeOutEffect();
            while(ScreenFadeEffect.AnimationIsPlaying)
            {
                yield return null;
            }
            string sceneName = ((Scene)_currentSubScene).name;
            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync((Scene)_currentSubScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
            _currentSubScene = null;
            ChangingScene = false;
            Debug.Log("Unloaded subscene: " + sceneName);
        }

        yield return null;
    }

    public void LoadSubScene(string sceneName)
    {
        if (_scenesNames.Contains(sceneName) && !ChangingScene)
        {
            StartCoroutine(LoadSubSceneAsync(sceneName));
        }
        else
        {
            Debug.Log("Scene " + sceneName + " hasn't been found");
        }
    }
}
