using UnityEngine;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    private void OnAsyncCompleted(AsyncOperation async)
    {
        async.completed -= OnAsyncCompleted;
    }

    private void OnSceneLoadCompleted(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.SetActiveScene(scene);

        SceneManager.sceneLoaded -= OnSceneLoadCompleted;
    }
}
