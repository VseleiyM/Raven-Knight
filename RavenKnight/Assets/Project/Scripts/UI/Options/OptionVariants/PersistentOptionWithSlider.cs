using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class PersistentOptionWithSlider
    {
        [SerializeField] private float defaultValue;
        [SerializeField] private Slider slider;
        [SerializeField] private string optionName;
        /// <summary>
        /// Инициализация полей вручную.
        /// </summary>
        /// <param name="slider"></param>
        /// <param name="optionName"></param>
        /// <param name="defaultValue"></param>
        public void Init(Slider slider, string optionName, float defaultValue)
        {
            this.slider = slider;
            this.defaultValue = defaultValue;
            this.optionName = optionName;
            SetPersistentValue();
            slider.onValueChanged.AddListener(OnValueChanged);
        }
        /// <summary>
        /// Инициализация при заданных из инспектора полях.
        /// </summary>
        public void Init()
        {
            Init(slider, optionName, defaultValue);
        }

        private void OnValueChanged(float value)
        {
            PlayerPrefs.SetFloat(optionName, value);
        }
        private void SetPersistentValue()
        {
            slider.value = PlayerPrefs.GetFloat(optionName, defaultValue);
        }
        /// <summary>
        /// Сбросить выбор на значение по умолчанию.
        /// Значение равно 0, т.е. первому наименованию из переданных.
        /// </summary>
        public void Reset()
        {
            slider.value = defaultValue;
            OnValueChanged(defaultValue);
        }
    }
}