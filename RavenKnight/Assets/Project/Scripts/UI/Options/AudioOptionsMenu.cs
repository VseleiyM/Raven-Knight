using System.Collections;
using Audio;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace UI
{
    public class AudioOptionsMenu : AbstractOptionsMenu
    {
        [SerializeField] private float volumeChangeTime = 0.5f;
        [Header("Sliders")]
        [SerializeField] private PersistentOptionWithSlider masterOption = new PersistentOptionWithSlider();
        [SerializeField] private PersistentOptionWithSlider soundOption = new PersistentOptionWithSlider();
        [SerializeField] private PersistentOptionWithSlider musicOption = new PersistentOptionWithSlider();
        [Header("Mixer")]
        [SerializeField] private AudioMixer audioMixer;

        [Inject] AudioController audioController;

        /// <summary>
        /// Класс для постепенного изменения громкости.
        /// </summary>
        private class ValueWithTimeChanger
        {
            /// <summary>
            /// Время изменения громкости.
            /// </summary>
            private float volumeChangeTime;
            /// <summary>
            /// Наименования параметра в микшере.
            /// </summary>
            public string volumeName { get; private set; }
            private PersistentOptionWithSlider audioOption;
            public ValueWithTimeChanger(float volumeChangeTime, PersistentOptionWithSlider audioOption, string volumeName)
            {
                this.volumeChangeTime = volumeChangeTime;
                this.audioOption = audioOption;
                lastValue = audioOption.currentValue;
                this.volumeName = volumeName;
            }
            /// <summary>
            /// Прошло времени с начала изменения громкости.
            /// </summary>
            private float volumeChangeTimeLeft;
            private float targetValue;
            public float currentValue { get; private set; }
            /// <summary>
            /// Последнее заданное значение громкости.
            /// </summary>
            private float lastValue;
            /// <summary>
            /// Установить новое значение, к которому надо стремиться.
            /// </summary>
            /// <param name="targetValue"></param>
            public void SetTargetValue(float targetValue)
            {
                lastValue = currentValue;
                this.targetValue = targetValue;
                volumeChangeTimeLeft = volumeChangeTime;
                isNeedChanged = true;
            }
            /// <summary>
            /// Установить новое значение, к которому надо стремиться
            /// из выданного слайдера.
            /// </summary>
            public void SetTargetValue()
            {
                SetTargetValue(audioOption.currentValue);
            }
            /// <summary>
            /// Еще надо продолжать изменение громкости.
            /// </summary>
            private bool isNeedChanged;
            /// <summary>
            /// Принудительно завершить изменение громкости.
            /// Текущая громкость сразу станет целевой.
            /// </summary>
            public void ForcedToComplete()
            {
                currentValue = targetValue;
                isNeedChanged = false;
            }
            /// <summary>
            /// Выполнить обновление громкости, приблизив текущую к целевой.
            /// </summary>
            /// <param name="deltaTime"></param>
            public void Update(float deltaTime)
            {
                if (isNeedChanged)
                {
                    if (Mathf.Abs(targetValue - currentValue) > 0.05f)
                    {
                        volumeChangeTimeLeft -= deltaTime;
                        currentValue = Mathf.Lerp(targetValue, lastValue, volumeChangeTimeLeft / volumeChangeTime);
                    }
                    else
                    {
                        ForcedToComplete();
                    }
                }
            }
        }

        private ValueWithTimeChanger masterChanger;
        private ValueWithTimeChanger soundChanger;
        private ValueWithTimeChanger musicChanger;
        private ValueWithTimeChanger[] changers;

        private string masterVolumeName = "MasterVolume";
        private string soundVolumeName = "SoundVolume";
        private string musicVolumeName = "MusicVolume";

        private void ChangeVolume(string nameVolume, float volumeValue)
        {
            if (volumeValue == 0)
            {
                audioMixer.SetFloat(nameVolume, -80);
            }
            else
            {
                audioMixer.SetFloat(nameVolume, Mathf.Lerp(-30, 0, volumeValue));
            }
        }


        protected override void OnResetOptions()
        {
            masterOption.Reset();
            soundOption.Reset();
            musicOption.Reset();
        }
        public override void Init(MenuController menuController)
        {
            base.Init(menuController);

            masterOption.Init();
            masterChanger = new ValueWithTimeChanger(volumeChangeTime, masterOption, masterVolumeName);
            soundOption.Init();
            soundChanger = new ValueWithTimeChanger(volumeChangeTime, soundOption, soundVolumeName);
            musicOption.Init();
            musicChanger = new ValueWithTimeChanger(volumeChangeTime, musicOption, musicVolumeName);

            changers = new ValueWithTimeChanger[]
            {
                masterChanger,
                soundChanger,
                musicChanger
            };

            masterOption.valueChanged += OnMasterValueChanged;
            soundOption.valueChanged += OnSoundValueChanged;
            musicOption.valueChanged += OnMusicValueChanged;

            //Выставить зачение громкости аудио при входе в игру с небольшой задержкой.
            Invoke(nameof(FirstSetAudioSettigns), 0.01f);
        }
        /// <summary>
        /// Выставить зачение громкости аудио при входе в игру.
        /// </summary>
        private void FirstSetAudioSettigns()
        {
            ForcedApplyAudioSettings();
        }

        private void OnMasterValueChanged(float value)
        {
           masterChanger.SetTargetValue(value);
        }
        private void OnSoundValueChanged(float value)
        {
            soundChanger.SetTargetValue(value);
        }
        private void OnMusicValueChanged(float value)
        {
            musicChanger.SetTargetValue(value);
        }
        /// <summary>
        /// Сразу передать значения ползунков звука в микшер,
        /// без постепенного увеличения.
        /// </summary>
        private void ForcedApplyAudioSettings()
        {
            foreach (ValueWithTimeChanger changer in changers)
            {
                changer.SetTargetValue();
                changer.ForcedToComplete();
                ChangeVolume(changer.volumeName, changer.currentValue);
            }
        }
        private void Update()
        {
            float deltaTime = Time.deltaTime;
            foreach(ValueWithTimeChanger changer in changers)
            {
                changer.Update(deltaTime);
                ChangeVolume(changer.volumeName, changer.currentValue);
            }
        }
        private void OnDisable()
        {
           ForcedApplyAudioSettings();
        }
    }
}