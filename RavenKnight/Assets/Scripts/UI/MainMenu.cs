using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.UI
{
    public class MainMenu : AbstractMenu
    {
        [Header("Buttons")]
        [SerializeField] private Button newGameButton = null;
        [SerializeField] private Button optionsButton = null;
        [SerializeField] private Button creditsButton = null;
        [SerializeField] private Button exitButton = null;

        private void CloseApplication()
        {
            Application.Quit();
        }

        private void StartNewGame()
        {
            SceneManager.LoadSceneAsync((int)EnumScenes.GameScene, LoadSceneMode.Additive);
            menuController.gameMode = EnumScenes.GameScene;
            OpenMenu(MenuType.loadScreen);
        }
        private void OpenOptions()
        {
            OpenMenu(MenuType.options);
        }
        private void OpenCredits()
        {
            OpenMenu(MenuType.credits);
        }
        public override void Init(MenuController menuController)
        {
            base.Init(menuController);
            newGameButton.onClick.AddListener(StartNewGame);
            optionsButton.onClick.AddListener(OpenOptions);
            creditsButton.onClick.AddListener(OpenCredits);
            exitButton.onClick.AddListener(CloseApplication);
        }
    }
}