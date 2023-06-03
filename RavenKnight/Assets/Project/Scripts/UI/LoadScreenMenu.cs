using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LoadScreenMenu : AbstractMenu
    {
        private void OnSceneLoadCompleted(Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneManager.SetActiveScene(scene);
            Time.timeScale = 1;
            SetActive(false);
        }

        public override void Init(MenuController menuController)
        {
            base.Init(menuController);
            SceneManager.sceneLoaded += OnSceneLoadCompleted;
        }
    }
}