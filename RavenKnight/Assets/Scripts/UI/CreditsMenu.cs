using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CreditsMenu : AbstractMenuWithBack
    {
        [Header("Links")]
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField, Min(0f)] private float timeBeforeStart;
        [SerializeField] private float speed;

        private Coroutine corScrollCredits;

        public override void Init(MenuController menuController)
        {
            base.Init(menuController);
        }

        private void OnEnable()
        {
            corScrollCredits = StartCoroutine(ScrollCredits(timeBeforeStart));
        }

        private void OnDisable()
        {
            if (corScrollCredits != null)
            {
                StopCoroutine(corScrollCredits);
                scrollRect.content.localPosition = Vector2.zero;
            }
        }

        private IEnumerator ScrollCredits(float timeBeforeStart)
        {
            float elapse = 0;
            while (elapse < timeBeforeStart)
            {
                elapse += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            float checkValue = Math.Abs(scrollRect.content.offsetMin.y);
            while (scrollRect.content.localPosition.y < checkValue)
            {
                scrollRect.content.localPosition = scrollRect.content.localPosition + Vector3.up * Time.deltaTime * speed;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}