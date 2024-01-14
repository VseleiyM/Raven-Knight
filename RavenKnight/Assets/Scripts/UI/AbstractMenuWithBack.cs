using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    /// <summary>
    /// Меню имеющее кнопку для возвращения к предыдущему меню.
    /// </summary>
    public abstract class AbstractMenuWithBack : AbstractMenu
    {
        [Header("Back button")]
        [SerializeField] private Button backButton = null;

        public override void Init(MenuController menuController)
        {
            base.Init(menuController);
            if (backButton == null)
            {
                Debug.LogError("Back button is not set!");
            }
            else
            {
                backButton.onClick.AddListener
                    (()=>
                    {
                        menuController.OpenPreviousMenu();
                    }
                    );
            }
        }
    }
}