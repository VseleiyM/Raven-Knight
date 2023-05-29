using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenu : AbstractMenu
    {
        [Header("Buttons")]
        [SerializeField] private Button continueButton = null;
        [SerializeField] private Button optionsButton = null;
        [SerializeField] private Button mainMenuButton = null;

        private void OpenOptions()
        {
            OpenMenu(MenuType.options);
        }
        private void OpenMainMenu()
        {
            menuController.gameMode = EnumScenes.MenuScene;
            OpenMenu(MenuType.mainMenu);
        }
        private void SetPause(bool isPause)
        {
            if(isPause)
            {
                Time.timeScale = 0;
                SetActive(true);
            }
            else
            {
                SetActive(false);
                Time.timeScale = 1;
            }
        }

        public override void Init(MenuController menuController)
        {
            base.Init(menuController);
            menuController.pauseKeyDowned += () => SetPause(true);
            continueButton.onClick.AddListener(()=>SetPause(false));
            optionsButton.onClick.AddListener(OpenOptions);
            mainMenuButton.onClick.AddListener(OpenMainMenu);
        }
    }
}
