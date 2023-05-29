using UnityEngine;

namespace UI
{
    public class SoundOptionMenu : AbstractOptionsMenu
    {
        [Space, Header("Sliders")]
        [SerializeField] private PersistentOptionWithSlider masterOption = new PersistentOptionWithSlider();
        [SerializeField] private PersistentOptionWithSlider soundOption = new PersistentOptionWithSlider();
        [SerializeField] private PersistentOptionWithSlider musicOption = new PersistentOptionWithSlider();

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
        }
    }
}