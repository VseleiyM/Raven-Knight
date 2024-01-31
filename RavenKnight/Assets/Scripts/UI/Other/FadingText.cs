using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.UI
{
    public class FadingText : MonoBehaviour
    {
        [SerializeField, Min(0.01f)] private float timeFading = 1;

        [SerializeField] private TextMeshProUGUI textMesh;

        private Coroutine coroutine;

        private void OnEnable()
        {
            coroutine = StartCoroutine(MainCoroutine());
            textMesh.alpha = 0;
        }

        private void OnDisable()
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        private IEnumerator MainCoroutine()
        {
            yield return null;

            bool show = false;
            while (true)
            {
                if (textMesh.alpha <= 0)
                    show = true;
                else if (textMesh.alpha >= 1)
                    show = false;

                if (show)
                    textMesh.alpha += Time.deltaTime / timeFading;
                else
                    textMesh.alpha -= Time.deltaTime / timeFading;

                yield return new WaitForEndOfFrame();
            }
        }
    }
}