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
    public UnityEngine.UI.Button audioSettButton;
    public UnityEngine.UI.Button videoSettButton;
    public UnityEngine.UI.Button backFromAButton;
    public UnityEngine.UI.Button backFromVButton;
    public UnityEngine.UI.Slider loadSlider;

    public GameObject menuObj;
    public GameObject settingsObj;
    public GameObject loadCanvas;
    public GameObject backImage;
    public GameObject settingButtObj;
    public GameObject audioSettObj;
    public GameObject videoSettObj;
    public GameObject loadText;
    public GameObject pressText;
    public GameObject nameText;
    
    // Start is called before the first frame update
    void Start()
    {
        newGameButton.onClick.AddListener(LoadNewScene);
        //loadButton.onClick.AddListener(LoadScene);
        settingsButton.onClick.AddListener(OpenSettings);
        menuButton.onClick.AddListener(CloseSettings);
        exitButton.onClick.AddListener(ExitScene);
        
        audioSettButton.onClick.AddListener(OpenAudioSettings);
        backFromAButton.onClick.AddListener(CloseAudioSettings);
        videoSettButton.onClick.AddListener(OpenVideSettings);
        backFromVButton.onClick.AddListener(CloseVideoSettings);
    }

    // Update is called once per frame
    private void LoadNewScene()
    {
        loadCanvas.SetActive(true);
        StartCoroutine(LoadAsync());
        menuObj.SetActive(false);
        backImage.SetActive(false);
        nameText.SetActive(false);
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
    
    private void OpenVideSettings()
    {
        settingButtObj.SetActive(false);
        videoSettObj.SetActive(true);
    }
    
    private void CloseVideoSettings()
    {
        settingButtObj.SetActive(true);
        videoSettObj.SetActive(false);
    }
    
    // с загрузкой сохранений
   /* private void LoadScene()
    {
        loadImage.SetActive(true);
        SceneManager.LoadSceneAsync("TestLevle");
        menuObj.SetActive(false);
        backImage.SetActive(false);
    }
    */
    
    private void ExitScene()
    {
        Application.Quit();
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TestLevle");

        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            loadSlider.value = asyncLoad.progress;
            if (loadSlider.value >= .9f && !asyncLoad.allowSceneActivation)
            {
                loadText.SetActive(false);
                pressText.SetActive(true);
                if (Input.anyKeyDown)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
