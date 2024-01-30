using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class DashProgressView : MonoBehaviour
    {
        [SerializeField] private FilledIcon filledIcon = null;
        [SerializeField] private TextMeshProUGUI text = null;

        public float filledValue
        {
            get => filledIcon.value;
            set => filledIcon.value = value;
        }

        public void SetReady(bool isReady)
        {
            Color color = text.color;
            text.color = new Color(color.r, color.g, color.b, isReady ? 1f : 0.3f);
        }
        public static DashProgressView instance { get; private set; }

        private void Awake()
        {
            instance = this;
            filledIcon.minValue = 0;
            filledIcon.maxValue = 1;
            SetReady(true);
            filledValue = filledIcon.maxValue;
        }
    }
}