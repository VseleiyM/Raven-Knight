using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UI.ButtonWithEvents;

namespace UI
{
    [RequireComponent(typeof(ButtonWithEvents))]
    public class SelectButtonScaler : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform = null;
        [SerializeField] private ButtonWithEvents buttonWithEvents = null;
        [SerializeField] private float scale = 1.2f;
        [SerializeField] private TextMeshProUGUI[] textFields = new TextMeshProUGUI[0];
        [SerializeField] private Image[] images = new Image[0];
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
            foreach(TextMeshProUGUI textField in textFields)
            {
                textField.color = enteredColor;
            }
            foreach(Image image in images)
            {
                image.color = enteredColor;
            }
        }
        /// <summary>
        /// Отменить увеличение.
        /// </summary>
        /// <param name="obj"></param>
        private void OnExited(PointerEventData obj)
        {
            rectTransform.localScale = originScale;

            foreach (TextMeshProUGUI textField in textFields)
            {
                textField.color = orginalColor;
            }
            foreach (Image image in images)
            {
                image.color = orginalColor;
            }
        }
        private void OnEnable()
        {
            PublicSelectionState state = buttonWithEvents.GetCurrentSelectionState();
            if (state == PublicSelectionState.Normal)
            {
                OnExited(null);
            }
            else
            {
                OnEntered(null);
            }
        }
        private void Awake()
        {
            buttonWithEvents.PointerEntered += OnEntered;
            buttonWithEvents.PointerExited += OnExited;
            originScale = rectTransform.localScale;
            calculateScale = new Vector3(scale, scale, scale);
        }
    }
}