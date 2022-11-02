using System.Collections;
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

    public Gun gun;
    public Test player;
    public GameObject pauseUI;
    public GameObject pauseObj;
    public GameObject settingsObj;
    public GameObject loadImage;

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
        SceneManager.LoadSceneAsync(0); 
        loadImage.SetActive(true);
    }
    
    private void ExitScene()
    {
        Application.Quit();
    }
}
