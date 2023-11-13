using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Скрипт для заполняемо иконки.
    /// </summary>
    public class FilledIcon : MonoBehaviour
    {
        [SerializeField] private Image icon = null;
        [SerializeField] private float minValue = 0;
        [SerializeField] private float maxValue = 100;

        /// <summary>
        /// Задать значение заполненности для иконки.
        /// </summary>
        private void SetIconFillAmount()
        {
            //float relativeMin = 0; <- из-за сдвига в последствии надо отнять минимум.
            float relativeMax = maxValue - minValue;
            float relativeCurrent = value - minValue;
            icon.fillAmount = relativeCurrent / relativeMax;
        }

        public event Action<float> valueChanged;
        /// <summary>
        /// Текущее значение заполнености.
        /// </summary>
        private float currentValue;
        /// <summary>
        ///  Текущее значение заполнености.
        ///  Не может быть больше максимума и меньше минимума.
        /// </summary>
        public float value
        {
            get
            {
                return currentValue;
            }
            set
            {
                currentValue = Mathf.Clamp(value, minValue, maxValue);
                SetIconFillAmount();
                valueChanged?.Invoke(currentValue);
            }
        }
        /// <summary>
        /// Погрешность при сравнении.
        /// Нужна из-за представления float в двоичном виде.
        /// </summary>
        private const float EPSILON = 0.001f;
        /// <summary>
        /// Значение достигло минимума.
        /// </summary>
        public bool isMin
        {
            get => Mathf.Abs(currentValue - minValue) < EPSILON;
        }
        /// <summary>
        /// Значение достигло максимума.
        /// </summary>
        public bool isMax
        {
            get => Mathf.Abs(currentValue - maxValue) < EPSILON;
        }
        /// <summary>
        /// Установить занчение в минимум.
        /// </summary>
        public void ToMinValue()
        {
            value = minValue;
        }
        /// <summary>
        /// Установить занчение в максимум.
        /// </summary>
        public void ToMaxValue()
        {
            value = maxValue;
        }
        private void Awake()
        {
            ToMinValue();
        }
    }
}