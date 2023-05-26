using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    public event Action allSubSceneClosed;

    public List<int> subScene;
    public List<int> tempScene;

    private void Awake()
    {
        instance = this;
    }

    public void CloseAllSubScene()
    {
        if (subScene.Count > 0)
        {
            CloseSubScene();
        }
    }

    private void CloseSubScene()
    {
        int lastScene = subScene[subScene.Count - 1];
        var async = SceneManager.UnloadSceneAsync(lastScene);
        async.completed += OnUnloadAsynceComplete;
        subScene.Remove(lastScene);
    }

    private void OnUnloadAsynceComplete(AsyncOperation oldAsync)
    {
        oldAsync.completed -= OnUnloadAsynceComplete;

        if (subScene.Count > 0)
        {
            CloseSubScene();
        }
        else
        {
            Debug.Log("allSubSceneClosed");
            allSubSceneClosed.Invoke();
        }
    }
}
