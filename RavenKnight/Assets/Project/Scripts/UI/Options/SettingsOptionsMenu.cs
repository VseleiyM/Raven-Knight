using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace UI
{
    public class SettingsOptionsMenu : AbstractOptionsMenu
    {

        [Space, Header("Choosers")]
        [SerializeField] private HorizontalChooser resolutionChooser = null;
        [SerializeField] private HorizontalChooser winModeChooser = null;
        [SerializeField] private HorizontalChooser languageChooser = null;
        [SerializeField] private HorizontalChooser bloodChooser = null;

        private PersistentOptionWithChooser resolutionOption = new PersistentOptionWithChooser();
        private PersistentOptionWithChooser winModeOption = new PersistentOptionWithChooser();
        private PersistentOptionWithChooser languageOption = new PersistentOptionWithChooser();
        private PersistentOptionWithChooser bloodOption = new PersistentOptionWithChooser();

        protected override void OnResetOptions()
        {
            resolutionOption.Reset();
            languageOption.Reset();
            bloodOption.Reset();
            currentResolutionIndex = resolutionOption.defaultIndex;
            winModeOption.Reset();
            OnWindowedModeChanged();
        }

        private void SetSreenSettins()
        {
            int width = resolutions[currentResolutionIndex].x;
            int height = resolutions[currentResolutionIndex].y;
            Screen.SetResolution(width, height, isFullScreen);
        }

        #region Разрешение экрана.

        private Vector2Int[] resolutions =
        {
            new Vector2Int(1280, 720),
            new Vector2Int(1366, 768),
            new Vector2Int(1920, 1080),
            new Vector2Int(2560, 1140),
            new Vector2Int(1920*2, 1080*2)
        };
        private int currentResolutionIndex;

        private void OnResolutionIndexChanged(int index)
        {
            currentResolutionIndex = index;
            SetSreenSettins();
        }

        #endregion Разрешение экрана.

        #region Полноэкранный режим.

        private bool isFullScreen = true;
        private const string FULL_SCREEN_TEXT = "Full screen";
        private const string WINDOWED_TEXT = "Windowed";
        private void OnWindowedModeChanged()
        {
            isFullScreen = winModeOption.currentName == FULL_SCREEN_TEXT;
            SetSreenSettins();
        }

        #endregion Полноэкранный режим.


        public override void Init(MenuController menuController)
        {
            base.Init(menuController);

            //Настройки для разрешения экрана.
            {
                //Создать строковые значения.
                string[] stringResolution = new string[resolutions.Length];
                for (int i = 0; i < resolutions.Length; i++)
                {
                    stringResolution[i] = $"{resolutions[i].x}x{resolutions[i].y}";
                }
                resolutionOption.Init(resolutionChooser, "Graphics.Resolution", stringResolution);
                //Найти индекс по умолчанию.
                for (int i = 0; i < resolutions.Length; i++)
                {
                    if (resolutions[i].x == 1920 && resolutions[i].y == 1080)
                    {
                        resolutionOption.defaultIndex = i;
                        break;
                    }
                }

                currentResolutionIndex = resolutionChooser.currentIndex;
                resolutionOption.indexChanged += OnResolutionIndexChanged;
            }

            languageOption.Init(languageChooser, "Game.Language", "Eng", "rus", "chi");
            bloodOption.Init(bloodChooser, "Game.Blood", "yes", "no");

            winModeOption.Init(winModeChooser, "Graphics.WinMode", FULL_SCREEN_TEXT, WINDOWED_TEXT);
            winModeOption.valueChanged += OnWindowedModeChanged;

            OnResolutionIndexChanged(resolutionOption.currentIndex);
            OnWindowedModeChanged();
        }
    }
}