using Extensions;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Скрипт для выбора, где можно с помощью кнопок 
    /// "влево"/"вправо" изменять выбранное наименование.
    /// </summary>
    public class HorizontalChooser : MonoBehaviour
    {
        [SerializeField] private Button leftButton = null;
        [SerializeField] private Button rightButton = null;
        [SerializeField] private TextMeshProUGUI chooseName = null;

        [SerializeField] private string[] namesForChoose = new string[0];

        /// <summary>
        /// Проверить зполненость наименований.
        /// </summary>
        /// <returns>true, если есть хоть одно наименование.</returns>
        private bool NamesForChooseCheck()
        {
            if (namesForChoose == null || namesForChoose.Length == 0)
            {
                ErrorMessage(nameof(namesForChoose));
                return false;
            }
            return true;
        }
        /// <summary>
        /// Установить имена для выбора.
        /// </summary>
        /// <param name="namesForChoose"></param>
        public void SetNamesForChoose(string[] namesForChoose)
        {
            this.namesForChoose = namesForChoose;
            if (NamesForChooseCheck())
            {
                chooseName.text = namesForChoose[0];
            }
        }
        /// <summary>
        /// Индекс выбранного наименования.
        /// </summary>
        public int currentIndex { get; private set; }
        /// <summary>
        /// Выбранное наименование.
        /// </summary>
        public string currentName
        {
            get => namesForChoose[currentIndex];
        }
        /// <summary>
        /// Выбор наименования изменился.
        /// В агрументе указан индекс наименования.
        /// </summary>
        public event Action<int> indexChanged;
        private void SetNameForChangedIndex()
        {
            chooseName.text = currentName;

            indexChanged?.Invoke(currentIndex);
        }
        public void SetIndex(int index)
        {
            if (currentIndex != index)
            {
                currentIndex = index;
                if (currentIndex >= namesForChoose.Length)
                {
                    currentIndex = 0;
                }
                else if (currentIndex < 0)
                {
                    currentIndex = namesForChoose.Length - 1;
                }

                SetNameForChangedIndex();
            }
        }
        /// <summary>
        /// Событие смены индекса.
        /// </summary>
        /// <param name="isLeft"></param>
        private void OnIndexChanged(bool isLeft)
        {
            if (!NamesForChooseCheck())
                return;

            int value = isLeft ? -1 : 1;
            SetIndex(currentIndex + value);
        }
        /// <summary>
        /// Сбросить инедекс на значение по умолчанию.
        /// Значение равно 0, т.е. первому наименованию из переданных.
        /// </summary>
        public void ResetIndexToDefault()
        {
            SetIndex(0);
        }

        private void ErrorMessage(string fieldName)
        {
            DebugLogExtensions.ErrorFieldIsNotSet(fieldName);
        }
        private void Awake()
        {
            if (leftButton == null)
                ErrorMessage(nameof(leftButton));
            else if (rightButton == null)
                ErrorMessage(nameof(rightButton));
            else if (chooseName == null)
                ErrorMessage(nameof(chooseName));
            else
            {
                leftButton.onClick.AddListener(() => OnIndexChanged(true));
                rightButton.onClick.AddListener(() => OnIndexChanged(false));
            }
        }
    }
}