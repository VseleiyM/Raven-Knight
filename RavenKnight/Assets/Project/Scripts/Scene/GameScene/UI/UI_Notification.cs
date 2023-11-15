using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UI_Notification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI notification;
    [SerializeField] private TextMeshProUGUI gainPoints;
    [SerializeField] private CanvasGroup canvasGroup;
    [Header("Настройки")]
    [SerializeField] [Min(0.01f)] private float fadeTime = 1;
    [SerializeField] [Min(0.01f)] private float lifeTime = 1;
    [Inject] private LocalizaiotnKeeper keeper;

    private Coroutine coroutine;
    private string textRoomCleared;
    private string textPoints;
    private string textWave;
    private string textBossWave;

    private void Awake()
    {
        GlobalEvents.openRoom += OnRoomOpen;
        GlobalEvents.nextWave += OnNextWave;
        keeper.languageChanged += OnLangaugeChanged;
    }

    private void OnDestroy()
    {
        GlobalEvents.openRoom -= OnRoomOpen;
        GlobalEvents.nextWave -= OnNextWave;
        keeper.languageChanged -= OnLangaugeChanged;
    }

    private void Start()
    {
        textRoomCleared = keeper.GetLocalization("GameMenu.UI.RoomCleared");
        textPoints = keeper.GetLocalization("GameMenu.UI.Points");
        textWave = keeper.GetLocalization("GameMenu.UI.Wave");
        textBossWave = keeper.GetLocalization("GameMunu.UI.BossWave");
        notification.font = keeper.currentFont;
        gainPoints.font = keeper.currentFont;
    }

    private void OnRoomOpen(int score)
    {
        GlobalEvents.SendScoreChanged(score);
        notification.text = $"{textRoomCleared}";
        gainPoints.text = $"+{score} {textPoints}";
        if (coroutine == null)
            coroutine = StartCoroutine(AnimationNotification());
    }

    private void OnNextWave(int index, bool bossRoom)
    {
        if (!bossRoom)
            notification.text = $"{textWave} {index}";
        else
            notification.text = $"{textBossWave}";
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

    private void OnLangaugeChanged()
    {
        textRoomCleared = keeper.GetLocalization("GameMenu.UI.RoomCleared");
        textPoints = keeper.GetLocalization("GameMenu.UI.Points");
        textWave = keeper.GetLocalization("GameMenu.UI.Wave");
        textBossWave = keeper.GetLocalization("GameMunu.UI.BossWave");
        notification.font = keeper.currentFont;
        gainPoints.font = keeper.currentFont;
    }
}
