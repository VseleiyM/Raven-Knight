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
            public ValueWithTimeChanger(float volumeChangeTime, float value, string volumeName)
            {
                this.volumeChangeTime = volumeChangeTime;
                lastValue = value;
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

        [Inject] AudioController audioController;

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
            masterChanger = new ValueWithTimeChanger(volumeChangeTime, masterOption.currentValue, masterVolumeName);
            soundOption.Init();
            musicOption.Init();

            changers = new ValueWithTimeChanger[]
            {
                masterChanger
            };

            masterOption.valueChanged += OnMasterValueChanged;
            soundOption.valueChanged += OnSoundValueChanged;
            musicOption.valueChanged += OnMusicValueChanged;

            Invoke("TestExemple", 0.01f);
        }

        private void TestExemple()
        {
            OnMasterValueChanged(PlayerPrefs.GetFloat("Audio.Master"));
            OnSoundValueChanged(PlayerPrefs.GetFloat("Audio.Sound"));
            OnMusicValueChanged(PlayerPrefs.GetFloat("Audio.Music"));
        }


        private void OnMasterValueChanged(float value)
        {
           // ChangeVolume(masterVolumeName, value);
           masterChanger.SetTargetValue(value);
        }
        private void OnSoundValueChanged(float value)
        {
            ChangeVolume(soundVolumeName, value);
        }
        private void OnMusicValueChanged(float value)
        {
            ChangeVolume(musicVolumeName, value);
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
            foreach (ValueWithTimeChanger changer in changers)
            {
                changer.ForcedToComplete();
                ChangeVolume(changer.volumeName, changer.currentValue);
            }
        }
    }
}