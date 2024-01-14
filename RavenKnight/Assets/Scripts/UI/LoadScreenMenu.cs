using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.UI
{
    public class LoadScreenMenu : AbstractMenu
    {
        [SerializeField] private TextMeshProUGUI pressAnyButtonTextField = null;

        private bool isSceneLoad;
        public override void SetActive(bool isActive)
        {
            pressAnyButtonTextField.gameObject.SetActive(false);
            isSceneLoad = false;
            base.SetActive(isActive);
        }

        private Scene loadedScene;
        private void OnSceneLoadCompleted(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (!isSceneLoad)
            {
                isSceneLoad = true;
                pressAnyButtonTextField.gameObject.SetActive(true);
                loadedScene = scene;
            }
        }

        public override void Init(MenuController menuController)
        {
            base.Init(menuController);
            SceneManager.sceneLoaded += OnSceneLoadCompleted;
        }

        private void Update()
        {
            if (isSceneLoad && Input.anyKeyDown)
            {
                SceneManager.SetActiveScene(loadedScene);
                Time.timeScale = 1;
                SetActive(false);
            }
        }
    }
}