using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class OptionsMenu : AbstractMenuWithBack
    {
        [Header("Buttons")]
        [SerializeField] private Button gameButton = null;
        [SerializeField] private Button audioButton = null;
        [SerializeField] private Button graphicsButton = null;
        [SerializeField] private Button settingsButton = null;

        public override void Init(MenuController menuController)
        {
            base.Init(menuController);

            gameButton.onClick.AddListener(OnGameOptionsOpen);
            audioButton.onClick.AddListener(OnAudioOptionsOpen);
            graphicsButton.onClick.AddListener(OnGraphicsOptionsOpen);
            settingsButton.onClick.AddListener(OnSettingsOptionsOpen);
        }

        private void OnSettingsOptionsOpen()
        {
            OpenMenu(MenuType.settingsOptions);
        }

        private void OnGraphicsOptionsOpen()
        {
            OpenMenu(MenuType.graphicsOptions);
        }

        private void OnAudioOptionsOpen()
        {
            OpenMenu(MenuType.soundOptions);
        }

        private void OnGameOptionsOpen()
        {
            OpenMenu(MenuType.gameOptions);
        }
    }
}