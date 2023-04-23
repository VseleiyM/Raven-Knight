using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private GameObject _inGameMenu;
        [SerializeField] private GameObject _settingsMenu;

        public void InGameMenuOpen()
        {
            _inGameMenu.SetActive(true);
            Time.timeScale = 0;
        }

        public void InGameMenuClose()
        {
            Time.timeScale = 1;
            _inGameMenu.SetActive(false);
        }

        public void SettingsMenuOpen()
        {
            _settingsMenu.SetActive(true);
        }

        public void SettingsMenuClose()
        {
            _settingsMenu.SetActive(false);
        }

        public void ExitToMainMenu()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}