using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    [SerializeField] private AudioListener audioListener;
    private void OnAsyncCompleted(AsyncOperation async)
    {
        audioListener.enabled = true;

        async.completed -= OnAsyncCompleted;
    }

    private void OnSceneLoadCompleted(Scene scene, LoadSceneMode loadSceneMode)
    {
        audioListener.enabled = false;
        SceneManager.SetActiveScene(scene);

        SceneManager.sceneLoaded -= OnSceneLoadCompleted;
    }
}
