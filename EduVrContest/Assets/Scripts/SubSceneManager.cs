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
        _currentSubScene = null;
        parameters = new LoadSceneParameters(LoadSceneMode.Additive);
        ChangingScene = false;
    }

    void Update()
    {
        
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
        if (ScenesNames.Contains(sceneName))
        {
            ChangingScene = true;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, parameters);
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone)
            {
                if (!ScreenFadeEffect.AnimationIsPlaying)
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
            _currentWorldController = GameObject.FindGameObjectWithTag("GameController");
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
        if (ScenesNames.Contains(sceneName))
        {
            StartCoroutine(LoadSubSceneAsync(sceneName));
        } else
        {
            Debug.Log("Scene " + sceneName + " hasn't been found");
        }
    }
}
