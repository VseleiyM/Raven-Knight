using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace UI
{
    public class GraphicsOptionsMenu : AbstractOptionsMenu
    {
        [Space, Header("Choosers")]
        [SerializeField] private HorizontalChooser resolutionChooser = null;
        [SerializeField] private HorizontalChooser winModeChooser = null;

        [Space, Header("Sliders")]
        [SerializeField] private PersistentOptionWithSlider brightnessOption = new PersistentOptionWithSlider();
        [SerializeField] private PersistentOptionWithSlider contrastOption = new PersistentOptionWithSlider();
        [SerializeField] private PersistentOptionWithSlider saturationOption = new PersistentOptionWithSlider();
        [SerializeField] private PersistentOptionWithSlider vignetteOption = new PersistentOptionWithSlider();

        #region Post processing

        [Inject] private Volume volume;
        private VolumeProfile volumeProfile
        {
            get => volume.profile;
        }
        /// <summary>
        /// Сила множетеля яркости. Чем больше,
        /// тем темнее/светлее крайние значения.
        /// </summary>
        private const float BIGHTNESS_STRENGE = 8F;
        private void OnBrightnessChanged(float value)
        {
            if (volumeProfile.TryGet(out ColorAdjustments colorAdjustments))
            {
                value -= 0.5f;//значение 0,5 должно быть 0 яркости
                colorAdjustments.postExposure.value = value * BIGHTNESS_STRENGE;
            }
        }
        private void OnContrastChanged(float value)
        {
            if (volumeProfile.TryGet(out ColorAdjustments colorAdjustments))
            {
                value -= 0.5f;//значение 0,5 должно быть 0 яркости
                int multiplier = 200;//Т.к. значения от -100 до 100, то нужен множитель
                colorAdjustments.contrast.value = value * multiplier;
            }
        }
        private void OnSatrationChanged(float value)
        {
            if (volumeProfile.TryGet(out ColorAdjustments colorAdjustments))
            {
                value -= 0.5f;//значение 0,5 должно быть 0 яркости
                int multiplier = 200;//Т.к. значения от -100 до 100, то нужен множитель
                colorAdjustments.saturation.value = value * multiplier;
            }
        }
        private void OnVignetteChanged(float value)
        {
            if (volumeProfile.TryGet(out Vignette vignette))
            {
                vignette.intensity.value = Mathf.Lerp(0.3f, 0.6f, value);
            }
        }

        #endregion Post processing

        private PersistentOptionWithChooser resolutionOption = new PersistentOptionWithChooser();
        private PersistentOptionWithChooser winModeOption = new PersistentOptionWithChooser();
        protected override void OnResetOptions()
        {
            brightnessOption.Reset();
            resolutionOption.Reset();
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

            winModeOption.Init(winModeChooser, "Graphics.WinMode", FULL_SCREEN_TEXT, WINDOWED_TEXT);
            winModeOption.valueChanged += OnWindowedModeChanged;

            brightnessOption.Init();
            brightnessOption.valueChanged += OnBrightnessChanged;
            contrastOption.Init();
            contrastOption.valueChanged += OnContrastChanged;
            saturationOption.Init();
            saturationOption.valueChanged += OnSatrationChanged;
            vignetteOption.Init();
            vignetteOption.valueChanged += OnVignetteChanged;

            OnResolutionIndexChanged(resolutionOption.currentIndex);
            OnWindowedModeChanged();
            OnBrightnessChanged(brightnessOption.currentValue);
            OnContrastChanged(contrastOption.currentValue);
            OnSatrationChanged(saturationOption.currentValue);
            OnVignetteChanged(vignetteOption.currentValue);
        }

    }
}