using Audio;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace UI
{
    public class AudioOptionsMenu : AbstractOptionsMenu
    {
        [Header("Sliders")]
        [SerializeField] private PersistentOptionWithSlider masterOption = new PersistentOptionWithSlider();
        [SerializeField] private PersistentOptionWithSlider soundOption = new PersistentOptionWithSlider();
        [SerializeField] private PersistentOptionWithSlider musicOption = new PersistentOptionWithSlider();
        [Header("Mixer")]
        [SerializeField] private AudioMixer audioMixer;

        private const string MASTER_NAME_VOLUME = "MasterVolume";
        private const string SOUND_NAME_VOLUME = "SoundVolume";
        private const string MUSIC_NAME_VOLUME = "MusicVolume";

        private void ChangeVolume(string nameVolume, float volumeValue)
        {
            if (volumeValue == 0)
            {
                audioMixer.SetFloat(nameVolume, -80);
            }
            else
            {
                audioMixer.SetFloat(nameVolume, Mathf.Lerp(-40, 0, volumeValue));
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
            soundOption.Init();
            musicOption.Init();

            masterOption.valueChanged += OnMasterValueChanged;
            soundOption.valueChanged += OnSoundValueChanged;
            musicOption.valueChanged += OnMusicValueChanged;
        }

        private void OnMasterValueChanged(float value)
        {
            ChangeVolume(MASTER_NAME_VOLUME, value);
        }
        private void OnSoundValueChanged(float value)
        {
            ChangeVolume(SOUND_NAME_VOLUME, value);
        }
        private void OnMusicValueChanged(float value)
        {
            ChangeVolume(MUSIC_NAME_VOLUME, value);
        }
    }
}