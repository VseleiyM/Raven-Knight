using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace UI
{
    /// <summary>
    /// Класс-расширение для кнопки, который позволяет подписаться на события взаимодействия с курсором.
    /// При надобности можно будет его модифицировать.
    /// </summary>
    public class ButtonWithEvents : Button
    {
        public event Action<PointerEventData> PointerDowned;
        public event Action<PointerEventData> PointerUped;
        public event Action<PointerEventData> PointerEntered;
        public event Action<PointerEventData> PointerExited;

        /// <summary>
        /// Перечисление состояний кнопки, 
        /// которое можно использовать вне класса кнопки.
        /// </summary>
        public enum PublicSelectionState
        {
            Normal = 0,
            Highlighted = 1,
            Pressed = 2,
            Selected = 3,
            Disabled = 4
        }
        /// <summary>
        /// Получить текущее состояние кнопки.
        /// </summary>
        /// <returns></returns>
        public PublicSelectionState GetCurrentSelectionState()
        {
            return (PublicSelectionState)((int)currentSelectionState);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            PointerDowned?.Invoke(eventData);
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            PointerUped?.Invoke(eventData);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            PointerEntered?.Invoke(eventData);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            PointerExited?.Invoke(eventData);
        }
    }
}