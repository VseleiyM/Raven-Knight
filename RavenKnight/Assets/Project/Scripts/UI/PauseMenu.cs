using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : AbstractMenu
    {
        [Header("Buttons")]
        [SerializeField] private Button continueButton = null;
        [SerializeField] private Button optionsButton = null;
        [SerializeField] private Button mainMenuButton = null;

        private void OnDestroy()
        {
            GlobalEvents.returnMenu -= OpenMainMenu;
        }

        private void OpenOptions()
        {
            OpenMenu(MenuType.options);
        }

        private void GameSceneClose()
        {
            SceneController.instance.allSubSceneClosed += OnAllSubSceneClosed;
            SceneController.instance.CloseAllSubScene();

            void OnAllSubSceneClosed()
            {
                var async = SceneManager.UnloadSceneAsync((int)EnumScenes.GameScene);

                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (scene.buildIndex == (int)EnumScenes.MenuScene)
                    {
                        SceneManager.SetActiveScene(scene);
                    }
                }

                SceneController.instance.allSubSceneClosed -= OnAllSubSceneClosed;
            }
        }
        private void OpenMainMenu()
        {
            GameSceneClose();
            menuController.gameMode = EnumScenes.MenuScene;
            Time.timeScale = 1;
            isPause = false;
            OpenMenu(MenuType.mainMenu);
        }
        private bool isPause = false;
        private void PauseChange()
        {
            if (!gameObject.activeSelf)
            {
                isPause = true;
                Time.timeScale = 0;
                OpenMenu(MenuType.menuPause);
                //SetActive(isPause);
                GlobalEvents.SendPauseStatus(isPause);
            }
            else
            {
                if (menuController.CurrentMenu.GetType() != typeof(PauseMenu)) return;

                isPause = false;
                Time.timeScale = 1;
                SetActive(isPause);
                GlobalEvents.SendPauseStatus(isPause);
            }
        }

        public override void Init(MenuController menuController)
        {
            base.Init(menuController);
            GlobalEvents.returnMenu += OpenMainMenu;
            menuController.pauseKeyDowned += () => PauseChange();
            continueButton.onClick.AddListener(() => PauseChange());
            optionsButton.onClick.AddListener(OpenOptions);
            mainMenuButton.onClick.AddListener(OpenMainMenu);
        }
    }
}
