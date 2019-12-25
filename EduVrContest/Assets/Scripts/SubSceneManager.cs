using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubSceneManager : MonoBehaviour
{
    public List<string> ScenesNames;
    private LoadSceneParameters parameters;
    private Scene? _currentSubScene;
    private GameObject _currentWorldController;
    private ISceneController _currentSubSceneController;
    public ScreenFadeEffect ScreenFadeEffect;
    public bool ChangingScene;

    void Start()
    {
        parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        ChangingScene = false;
    }

    void Update()
    {
        
    }

    private IEnumerator LoadSubSceneAsync(string sceneName)
    {
        while(ChangingScene)
        {
            yield return null;
        }
        if (ScenesNames.Contains(sceneName))
        {
            ChangingScene = true;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, parameters);
            while (!asyncOperation.isDone)
            {
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
            _currentWorldController = GameObject.FindGameObjectWithTag("GameController");
            ScreenFadeEffect.FadeInEffect();
            while (!ScreenFadeEffect.AnimationFinished)
            {
                yield return null;
            }
            ChangingScene = false;
            Debug.Log("Loaded subscene: " + ((Scene)_currentSubScene).name);
        }

        yield return null;
    }

    private IEnumerator UnloadCurrentSubSceneAsync()
    {
        while (ChangingScene)
        {
            yield return null;
        }
        if (_currentSubScene != null)
        {
            ChangingScene = true;
            ScreenFadeEffect.FadeOutEffect();
            while(!ScreenFadeEffect.AnimationFinished)
            {
                yield return null;
            }
            _currentWorldController.GetComponent<ISceneController>().EndScene();
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
        if (_currentSubScene != null)
        {
            UnloadCurrentSubScene();
        }
        if (ScenesNames.Contains(sceneName))
        {
            StartCoroutine(LoadSubSceneAsync(sceneName));
        } else
        {
            Debug.Log("Scene " + sceneName + " hasn't been found");
        }
    }

    public void UnloadCurrentSubScene()
    {
        StartCoroutine(UnloadCurrentSubSceneAsync());
    }
}
