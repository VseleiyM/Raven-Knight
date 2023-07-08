using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(ButtonWithEvents))]
    public class SelectButtonScaler : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform = null;
        [SerializeField] private ButtonWithEvents buttonWithEvents = null;
        [SerializeField] private float scale = 1.2f;
        [SerializeField] private TextMeshProUGUI textFiled = null;
        [SerializeField] private Color orginalColor = Color.white;
        [SerializeField] private Color enteredColor = Color.white;

        /// <summary>
        /// Исходное увеличение.
        /// </summary>
        private Vector3 originScale;
        /// <summary>
        /// Посчитанное увеличение.
        /// </summary>
        private Vector3 calculateScale;
        /// <summary>
        /// Применить увеличение.
        /// </summary>
        /// <param name="obj"></param>
        private void OnEntered(PointerEventData obj)
        {
            rectTransform.localScale = calculateScale;
            textFiled.color = enteredColor;
        }
        /// <summary>
        /// Отменить увеличение.
        /// </summary>
        /// <param name="obj"></param>
        private void OnExited(PointerEventData obj)
        {
            rectTransform.localScale = originScale;
            textFiled.color = orginalColor;
        }
        private void Awake()
        {
            buttonWithEvents.PointerEntered += OnEntered;
            buttonWithEvents.PointerExited += OnExited;
            textFiled.color = orginalColor;
            originScale = rectTransform.localScale;
            calculateScale = new Vector3(scale, scale, scale);
        }
    }
}