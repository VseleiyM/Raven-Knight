using TMPro;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string key = string.Empty;
        [SerializeField] private bool isLocalized = true;
        [Inject] private LocalizaiotnKeeper keeper;

        private TextMeshProUGUI textFieldUI;
        private TextMeshPro textField;

        private void Awake()
        {
            if (keeper == null)
                keeper = LocalizaiotnKeeper.instance;
        }

        private void Start()
        {
            if (isLocalized)
            {
                textFieldUI = GetComponent<TextMeshProUGUI>();
                if (textFieldUI == null)
                    textField = GetComponent<TextMeshPro>();
                OnTextSetForLangauge();
                keeper.languageChanged += OnTextSetForLangauge;
            }
        }

        private void OnDestroy()
        {
            if (isLocalized)
                keeper.languageChanged -= OnTextSetForLangauge;
        }

        private void OnTextSetForLangauge()
        {
            string text = keeper.GetLocalization(key);
            if (!string.IsNullOrEmpty(text))
            {
                if (textFieldUI != null)
                {
                    textFieldUI.text = text;
                    textFieldUI.font = keeper.currentFont;
                }
                else
                {
                    textField.text = text;
                    textField.font = keeper.currentFont;
                }
            }
        }
    }
}