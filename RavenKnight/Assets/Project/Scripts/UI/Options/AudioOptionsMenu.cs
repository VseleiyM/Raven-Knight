using Audio;
using UnityEngine;
using Zenject;

namespace UI
{
    public class AudioOptionsMenu : AbstractOptionsMenu
    {
        [Space, Header("Sliders")]
        [SerializeField] private PersistentOptionWithSlider masterOption = new PersistentOptionWithSlider();
        [SerializeField] private PersistentOptionWithSlider soundOption = new PersistentOptionWithSlider();
        [SerializeField] private PersistentOptionWithSlider musicOption = new PersistentOptionWithSlider();

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
        }

        private void OnMasterValueChanged(float value)
        {
            AudioSource source = audioController.GetSource(AudioSourceType.generalMusic);
            source.volume = value;
        }
    }
}