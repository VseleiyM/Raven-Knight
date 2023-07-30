using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LocalizaiotnKeeper : MonoBehaviour
    {
        [SerializeField] private TextAsset csvFile = null;
        [SerializeField] private LanguageID id = LanguageID.russian;
        [SerializeField] private LanguageFont[] fonts = new LanguageFont[0];


        [Serializable]
        private class LanguageFont
        {
            public bool isDefault = false;
            public LanguageID id;
            public TMP_FontAsset font = null; 
        }
        public TMP_FontAsset currentFont { get; private set; }
        public void SetCurrentFont()
        {
            LanguageFont languageFont = fonts.FirstOrDefault(f => f.id == currentLanguageID);
            if (languageFont == null)
            {
                currentFont = fonts.FirstOrDefault(f => f.isDefault).font;
            }
            else
            {
                currentFont = languageFont.font;
            }
        }

        public LanguageID currentLanguageID { get; private set; }
        private SVCParser parser;
        /// <summary>
        /// словарь, хранящий локализации для указанных ключей.
        /// </summary>
        private Dictionary<string, string> localizationData = new Dictionary<string, string>();
        /// <summary>
        /// Язык локализации сменился.
        /// </summary>
        public event Action languageChanged;
        /// <summary>
        /// Сменить язык локализации.
        /// </summary>
        /// <param name="id"></param>
        public void SetLanguageID(LanguageID id)
        {
            currentLanguageID = id;
            localizationData.Clear();
            int intID = (int)id;
            for (int i = 1; i < parser.rows.Count; i++)
            {
                localizationData.Add(parser.rows[i][1], parser.rows[i][intID]);
            }
            SetCurrentFont();
            languageChanged?.Invoke();
        }
        public string GetLocalization(string key)
        {
            if (localizationData.ContainsKey(key))
                return localizationData[key];
            else
                return string.Empty;
        }
        private void Awake()
        {
            parser = new SVCParser(csvFile);
            SetLanguageID(id);
        }
    }
}