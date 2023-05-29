using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LoadScreenMenu : AbstractMenu
    {
        private AudioListener audioListener;
        private void OnSceneLoadCompleted(Scene scene, LoadSceneMode loadSceneMode)
        {
            audioListener.enabled = false;
            SceneManager.SetActiveScene(scene);
            SetActive(false);
        }

        public override void Init(MenuController menuController)
        {
            base.Init(menuController);
            SceneManager.sceneLoaded += OnSceneLoadCompleted;
            audioListener = GameObject.Find("MainMenuCamera").GetComponent<AudioListener>();
        }
    }
}