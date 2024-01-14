using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    public class SettingsOptionsMenu : AbstractOptionsMenu
    {

        [Space, Header("Choosers")]
        [SerializeField] private HorizontalChooser resolutionChooser = null;
        [SerializeField] private HorizontalChooser winModeChooser = null;
        [SerializeField] private HorizontalChooser languageChooser = null;
        [SerializeField] private HorizontalChooser bloodChooser = null;

        [Inject] private LocalizaiotnKeeper localizationKeeper;

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

        #region Localization

        private string[] languageNames;
        private LanguageID[] languageValues;
        private void SetLaguagesNames()
        {
            LanguageID[] values = (LanguageID[])Enum.GetValues(typeof(LanguageID));
            languageValues = values;

            languageNames = new string[languageValues.Length];
            for (int i = 0; i < values.Length; i++)
            {
                languageNames[i] = values[i].ToString();
            }
        }
        private void OnLanguageChanged(int index)
        {
            localizationKeeper.SetLanguageID(languageValues[index]);
        }

        #endregion Localization

        #region Разрешение экрана.

        private Vector2Int[] resolutions =
        {
            new Vector2Int(1280, 720),
            new Vector2Int(1366, 768),
            new Vector2Int(1920, 1080),
            new Vector2Int(2560, 1440),
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
        private const string FULL_SCREEN_TEXT = "Menu.Options.Settings.FullScreen";
        private const string WINDOWED_TEXT = "Menu.Options.Settings.Window";
        private void OnWindowedModeChanged()
        {
            isFullScreen = winModeOption.currentName == localizationKeeper.GetLocalization(FULL_SCREEN_TEXT);
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

            SetLaguagesNames();
            languageOption.Init(languageChooser, "Game.Language", languageNames);
            languageOption.indexChanged += OnLanguageChanged;
            OnLanguageChanged(languageOption.currentIndex);

            bloodOption.Init(bloodChooser, "Game.Blood", "yes", "no");

            winModeOption.Init(winModeChooser, "Graphics.WinMode", localizationKeeper.GetLocalization(FULL_SCREEN_TEXT), localizationKeeper.GetLocalization(WINDOWED_TEXT));
            winModeOption.valueChanged += OnWindowedModeChanged;

            OnResolutionIndexChanged(resolutionOption.currentIndex);
            OnWindowedModeChanged();
        }

        private void Awake()
        {
            localizationKeeper.languageChanged += OnLangaugeChanged;
        }

        private void OnDestroy()
        {
            localizationKeeper.languageChanged -= OnLangaugeChanged;
        }

        private void OnLangaugeChanged()
        {
            winModeOption.Init(winModeChooser, "Graphics.WinMode", localizationKeeper.GetLocalization(FULL_SCREEN_TEXT), localizationKeeper.GetLocalization(WINDOWED_TEXT));
        }
    }
}