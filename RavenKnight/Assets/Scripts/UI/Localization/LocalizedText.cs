using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string key = string.Empty;
        [SerializeField] private bool isLocalized = true;
        [Inject] private LocalizaiotnKeeper keeper;

        private TextMeshProUGUI textField;

        private void OnTextSetForLangauge()
        {
            string text = keeper.GetLocalization(key);
            if(!string.IsNullOrEmpty(text))
            {
                textField.text = text;
                textField.font = keeper.currentFont;
            }
        }
        private void Awake()
        {
            if (isLocalized)
            {
                textField = GetComponent<TextMeshProUGUI>();
                OnTextSetForLangauge();
                keeper.languageChanged += OnTextSetForLangauge;
            }
        }
        private void OnDestroy()
        {
            if (isLocalized)
                keeper.languageChanged -= OnTextSetForLangauge;

        }
    }
}