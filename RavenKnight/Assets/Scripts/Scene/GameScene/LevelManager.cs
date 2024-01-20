using Project;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<string> listLevels;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        GlobalEvents.SendChangeMusic(Project.Audio.MusicType.travelMusic);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.LoadSceneAsync(listLevels[0], LoadSceneMode.Additive);

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
