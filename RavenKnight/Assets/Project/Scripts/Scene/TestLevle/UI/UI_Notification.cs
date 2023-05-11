using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Notification : MonoBehaviour
{
    [SerializeField] private Text notification;
    [SerializeField] private Text gainPoints;
    [SerializeField] private CanvasGroup canvasGroup;
    [Header("Настройки")]
    [SerializeField] [Min(0.01f)] private float fadeTime = 1;
    [SerializeField] [Min(0.01f)] private float lifeTime = 1;

    private Coroutine coroutine;

    private void OnEnable()
    {
        GlobalEvents.openRoom += OnRoomOpen;
        GlobalEvents.nextWave += OnNextWave;
    }

    private void OnDisable()
    {
        GlobalEvents.openRoom -= OnRoomOpen;
        GlobalEvents.nextWave -= OnNextWave;
    }

    private void OnRoomOpen()
    {
        GlobalEvents.SendScoreChanged(5000);
        notification.text = $"ROOM CLEARED";
        gainPoints.text = $"+5000 Points";
        if (coroutine == null)
            coroutine = StartCoroutine(AnimationNotification());
    }

    private void OnNextWave(int index)
    {
        notification.text = $"WAVE {index}";
        gainPoints.text = $"";
        if (coroutine == null)
            coroutine = StartCoroutine(AnimationNotification());
    }

    private IEnumerator AnimationNotification()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.fixedDeltaTime / fadeTime;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSecondsRealtime(lifeTime);

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.fixedDeltaTime / fadeTime;
            yield return new WaitForFixedUpdate();
        }

        coroutine = null;
    }
}
