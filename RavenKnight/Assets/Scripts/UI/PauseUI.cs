﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public UnityEngine.UI.Button continueButton;
    public UnityEngine.UI.Button settingsButton;
    public UnityEngine.UI.Button backToPauseButton;
    public UnityEngine.UI.Button backToMenuButton;
    public UnityEngine.UI.Button exitButton;
    public UnityEngine.UI.Button audioSettButton;
    public UnityEngine.UI.Button videoSettButton;
    public UnityEngine.UI.Button backFromAButton;
    public UnityEngine.UI.Button backFromVButton;
    public UnityEngine.UI.Slider loadSlider;

    public Gun gun;
    public Test player;
    public GameObject pauseUI;
    public GameObject pauseObj;
    public GameObject settingsObj;
    public GameObject loadCanvas;
    public GameObject settingButtObj;
    public GameObject audioSettObj;
    public GameObject videoSettObj;
    

    public static bool gameIsPaused;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        gun.lookAtCursor = true;
        continueButton.onClick.AddListener(ClosePause);
        settingsButton.onClick.AddListener(OpenSettings);
        backToPauseButton.onClick.AddListener(CloseSettings);
        backToMenuButton.onClick.AddListener(BackToMainMenu);
        exitButton.onClick.AddListener(ExitScene);
        audioSettButton.onClick.AddListener(OpenAudioSettings);
        backFromAButton.onClick.AddListener(CloseAudioSettings);
        videoSettButton.onClick.AddListener(OpenVideoSettings);
        backFromVButton.onClick.AddListener(CloseVideoSettings);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ClosePause();
            }
            else
            {
                OpenPause();
            }
        }
        
    }

    private void OpenPause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        gun.lookAtCursor = false;
    }
    
    private void ClosePause()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        gun.lookAtCursor = true;
    }

    private void OpenSettings()
    {
        pauseObj.SetActive(false);
        settingsObj.SetActive(true);
    }

    private void CloseSettings()
    {
        settingsObj.SetActive(false);
        pauseObj.SetActive(true);
    }

    private void BackToMainMenu()
    {
        loadCanvas.SetActive(true);
        StartCoroutine(LoadAsync());
    }

    private void OpenAudioSettings()
    {
        settingButtObj.SetActive(false);
        audioSettObj.SetActive(true);
    }

    private void CloseAudioSettings()
    {
        settingButtObj.SetActive(true);
        audioSettObj.SetActive(false);
    }
    
    private void OpenVideoSettings()
    {
        settingButtObj.SetActive(false);
        videoSettObj.SetActive(true);
    }
    
    private void CloseVideoSettings()
    {
        settingButtObj.SetActive(true);
        videoSettObj.SetActive(false);
    }
    private void ExitScene()
    {
        Application.Quit();
    }
    
    IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MenuScene");
        
        while (!asyncLoad.isDone)
        {
            loadSlider.value = asyncLoad.progress;

            yield return null;
        }
    }
}
