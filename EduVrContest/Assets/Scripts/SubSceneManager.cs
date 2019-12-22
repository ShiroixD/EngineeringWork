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

    void Start()
    {
        parameters = new LoadSceneParameters(LoadSceneMode.Additive);
    }

    void Update()
    {
        
    }

    private IEnumerator LoadSubSceneAsync(string sceneName)
    {
        yield return null;

        if (ScenesNames.Contains(sceneName))
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, parameters);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
            _currentSubScene = SceneManager.GetSceneByName(sceneName);
            GameObject physicsCallbacksProvider = GameObject.Find("Physics Callbacks Provider");
            GameObject sceneController = GameObject.FindGameObjectWithTag("GameController");
            physicsCallbacksProvider.transform.parent = sceneController.transform;
            SceneManager.SetActiveScene((Scene)_currentSubScene);
            _currentWorldController = GameObject.FindGameObjectWithTag("GameController");
            Debug.Log("Loaded subscene: " + ((Scene)_currentSubScene).name);
        }
    }

    private IEnumerator UnloadCurrentSubSceneAsync()
    {
        yield return null;

        if (_currentSubScene != null)
        {
            _currentWorldController.GetComponent<ISceneController>().EndScene();
            string sceneName = ((Scene)_currentSubScene).name;
            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync((Scene)_currentSubScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
            _currentSubScene = null;
            Debug.Log("Unloaded subscene: " + sceneName);
        }
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
