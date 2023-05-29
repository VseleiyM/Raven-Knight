using UnityEngine;

namespace UI
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

        private void OnIndexChanged(int index)
        {
            PlayerPrefs.SetInt(optionName, chooser.currentIndex);
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