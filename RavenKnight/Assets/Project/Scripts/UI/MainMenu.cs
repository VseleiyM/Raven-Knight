using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : AbstractMenu
    {
        [Header("Buttons")]
        [SerializeField] private Button newGameButton = null;
        [SerializeField] private Button optionsButton = null;
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
        public override void Init(MenuController menuController)
        {
            base.Init(menuController);
            newGameButton.onClick.AddListener(StartNewGame);
            optionsButton.onClick.AddListener(OpenOptions);
            exitButton.onClick.AddListener(CloseApplication);
        }
    }
}