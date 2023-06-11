using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    [RequireComponent(typeof(ButtonWithEvents))]
    public class SelectButtonScaler : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform = null;
        [SerializeField] private ButtonWithEvents buttonWithEvents = null;
        [SerializeField] private float scale = 1.2f;

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
        private void OnScaled(PointerEventData obj)
        {
            rectTransform.localScale = calculateScale;
        }
        /// <summary>
        /// Отменить увеличение.
        /// </summary>
        /// <param name="obj"></param>
        private void OnScaleCanceled(PointerEventData obj)
        {
            rectTransform.localScale = originScale;
        }
        private void Awake()
        {
            buttonWithEvents.PointerEntered += OnScaled;
            buttonWithEvents.PointerExited += OnScaleCanceled;
            originScale = rectTransform.localScale;
            calculateScale = new Vector3(scale, scale, scale);
        }

    }
}