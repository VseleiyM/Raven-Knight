﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OptionsMenu : AbstractMenuWithBack
    {
        [Header("Buttons")]
        [SerializeField] private Button gameButton = null;
        [SerializeField] private Button audioButton = null;
        [SerializeField] private Button graphicsButton = null;

        public override void Init(MenuController menuController)
        {
            base.Init(menuController);

            gameButton.onClick.AddListener(OnGameOptionsOpen);
            audioButton.onClick.AddListener(OnAudioOptionsOpen);
            graphicsButton.onClick.AddListener(OnGraphicsOptionsOpen);
        }

        private void OnGraphicsOptionsOpen()
        {
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