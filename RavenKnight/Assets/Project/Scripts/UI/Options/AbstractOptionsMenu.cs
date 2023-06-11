using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Общий класс для всех меню настроек.
    /// </summary>
    public abstract class AbstractOptionsMenu : AbstractMenuWithBack
    {
        [Header("Reset button")]
        [SerializeField] private Button resetButton = null;

        public override void Init(MenuController menuController)
        {
            base.Init(menuController);
            if (resetButton == null)
            {
                DebugLogExtensions.ErrorFieldIsNotSet(nameof(resetButton));
            }
            else
            {
                resetButton.onClick.AddListener(OnResetOptions);
            }
        }
        protected abstract void OnResetOptions();
    }
}