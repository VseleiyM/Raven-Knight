using System;
using UnityEngine;

namespace Project.UI
{
    /// <summary>
    /// Класс, который упрощает работы с интерфейсом выбора, 
    /// самостоятельно сохраняя значения, которые были выбраны.
    /// </summary>
    public class PersistentOptionWithChooser
    {
        public string[] namesForChoose { get; private set; }
        private HorizontalChooser chooser;
        /// <summary>
        /// Имя для сохрание. Нужно, чтобы оно было уникальным.
        /// </summary>
        private string optionName;
        public void Init(HorizontalChooser chooser, string optionName, params string[] namesForChoose)
        {
            this.namesForChoose = namesForChoose;
            this.chooser = chooser;
            this.optionName = optionName;
            chooser.SetNamesForChoose(namesForChoose);
            SetPersistentValue();
            chooser.indexChanged += OnIndexChanged;
        }

        /// <summary>
        /// Индекс для настройки по умолчанию.
        /// </summary>
        public int defaultIndex
        {
            get => chooser.defaultIndex;
            set => chooser.defaultIndex = value;
        }
        /// <summary>
        /// Индекс выбранной настройки.
        /// </summary>
        public int currentIndex
        {
            get => chooser.currentIndex;
        }

        /// <summary>
        /// Выбранное в опции наименование.
        /// </summary>
        public string currentName
        {
            get => chooser.currentName;
        }
        /// <summary>
        /// Событие изменения выранного индекса.
        /// </summary>
        public event Action<int> indexChanged;
        /// <summary>
        /// Событие изменения выбора.
        /// </summary>
        public event System.Action valueChanged;
        private void OnIndexChanged(int index)
        {
            PlayerPrefs.SetInt(optionName, chooser.currentIndex);
            indexChanged?.Invoke(index);
            valueChanged?.Invoke();
        }
        private void SetPersistentValue()
        {
            int value = PlayerPrefs.GetInt(optionName, 0);
            chooser.SetIndex(value);
        }
        /// <summary>
        /// Сбросить выбор на значение по умолчанию.
        /// Значение равно 0, т.е. первому наименованию из переданных.
        /// </summary>
        public void Reset()
        {
            chooser.ResetIndexToDefault();
        }
    }
}