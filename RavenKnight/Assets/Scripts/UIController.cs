using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    public UnityEngine.UI.Button newGameButton;
    public UnityEngine.UI.Button loadButton;
    public UnityEngine.UI.Button settingsButton;
    public UnityEngine.UI.Button exitButton;
    public UnityEngine.UI.Button menuButton;
    public UnityEngine.UI.Button backToMenuButton;

    public GameObject menuObj;
    public GameObject settingsObj;
    public GameObject loadImage;
    public GameObject backImage;
    
    
    // Start is called before the first frame update
    void Start()
    {
        newGameButton.onClick.AddListener(LoadNewScene);
        loadButton.onClick.AddListener(LoadScene);
        settingsButton.onClick.AddListener(OpenSettings);
        menuButton.onClick.AddListener(CloseSettings);
        exitButton.onClick.AddListener(ExitScene);
        backToMenuButton.onClick.AddListener(BackToMenu);
    }

    // Update is called once per frame
    private void LoadNewScene()
    {
        loadImage.SetActive(true);
        SceneManager.LoadSceneAsync("RavenKnight1");
        menuObj.SetActive(false);
        backImage.SetActive(false);
    }

    private void OpenSettings()
    {
        menuObj.SetActive(false);
        settingsObj.SetActive(true);
    }

    private void CloseSettings()
    {
       settingsObj.SetActive(false);
       menuObj.SetActive(true);
    }

    // с загрузкой сохранений
    private void LoadScene()
    {
        loadImage.SetActive(true);
        SceneManager.LoadSceneAsync("RavenKnight1");
        menuObj.SetActive(false);
        backImage.SetActive(false);
    }

    private void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MenuScene");
    }
    
    private void ExitScene()
    {
        Application.Quit();
    }
}
