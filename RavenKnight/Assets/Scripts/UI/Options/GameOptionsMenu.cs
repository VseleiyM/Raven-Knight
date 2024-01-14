using UnityEngine;

namespace Project.UI
{
    public class GameOptionsMenu : AbstractOptionsMenu
    {
        [Space, Header("Choosers")]
        [SerializeField] private HorizontalChooser languageChooser = null;
        [SerializeField] private HorizontalChooser subtitlesChooser = null;
        [SerializeField] private HorizontalChooser bloodChooser = null;

        private PersistentOptionWithChooser languageOption = new PersistentOptionWithChooser();
        private PersistentOptionWithChooser subtitlesOption = new PersistentOptionWithChooser();
        private PersistentOptionWithChooser bloodOption = new PersistentOptionWithChooser();

        protected override void OnResetOptions()
        {
            languageOption.Reset();
            subtitlesOption.Reset();
            bloodOption.Reset();
        }
        public override void Init(MenuController menuController)
        {
            base.Init(menuController);

            languageOption.Init(languageChooser, "Game.Language", "Eng", "rus", "chi");
            subtitlesOption.Init(subtitlesChooser, "Game.Subtitle", "yes", "no");
            bloodOption.Init(bloodChooser, "Game.Blood", "yes", "no");
        }
    }
}