using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject loadScreen;
    [SerializeField] private AudioListener audioListener;
    [Space(10)]
    [SerializeField] private List<GameObject> allButtons;
    [SerializeField] private List<GameObject> disableInMenu;
    [SerializeField] private List<GameObject> disableInGame;

    private void Awake()
    {
        foreach (var go in allButtons)
        { go.SetActive(true); }
        foreach (var go in disableInMenu)
        { go.SetActive(false); }
    }

    public void NewGame()
    {
        loadScreen.SetActive(true);
        foreach (var go in allButtons)
        { go.SetActive(true); }
        foreach (var go in disableInGame)
        { go.SetActive(false); }

        SceneManager.LoadSceneAsync((int)EnumScenes.GameScene, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoadCompleted;
    }

    public void BackMenu()
    {
        
        foreach (var go in allButtons)
        { go.SetActive(true); }
        foreach (var go in disableInMenu)
        { go.SetActive(false); }

        var async = SceneManager.UnloadSceneAsync((int)EnumScenes.GameScene);
        async.completed += OnAsyncCompleted;

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.buildIndex == (int)EnumScenes.MenuScene)
            {
                SceneManager.SetActiveScene(scene);
            }
        }
    }
    
    public void LoadGame()
    {
        
    }
    
    public void CloseApplication()
    {
        Application.Quit();
    }

    private void OnAsyncCompleted(AsyncOperation async)
    {
        audioListener.enabled = true;

        async.completed -= OnAsyncCompleted;
    }

    private void OnSceneLoadCompleted(Scene scene, LoadSceneMode loadSceneMode)
    {
        loadScreen.SetActive(false);
        audioListener.enabled = false;
        SceneManager.SetActiveScene(scene);

        SceneManager.sceneLoaded -= OnSceneLoadCompleted;
    }
}
