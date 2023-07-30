using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UI_Score : MonoBehaviour
{
    [SerializeField] private TypePickupItem pickupItem;
    [SerializeField] private GameObject textScorePrefab;
    [SerializeField] private TextMeshProUGUI text_Score;

    [Inject] private LocalizaiotnKeeper keeper;
    private int score = 0;
    private string scoreTranslate;

    private void Start()
    {
        scoreTranslate = keeper.GetLocalization("GameMenu.UI.Score");
        text_Score.text = $"{scoreTranslate} : 0000000";
        text_Score.font = keeper.currentFont;
    }

    private void OnEnable()
    {
        GlobalEvents.itemHasPickup += OnItemHasPickup;
        GlobalEvents.scoreChanged += OnScoreChanged;
        GlobalEvents.createScoreText += OnCreateScoreText;
        keeper.languageChanged += OnLangaugeChanged;
    }
    
    private void OnDisable()
    {
        GlobalEvents.itemHasPickup -= OnItemHasPickup;
        GlobalEvents.scoreChanged -= OnScoreChanged;
        GlobalEvents.createScoreText -= OnCreateScoreText;
        keeper.languageChanged -= OnLangaugeChanged;

    }

    private void OnItemHasPickup(PickupItem item)
    {
        if (item.TypePickupItem != pickupItem) return;

        score += item.Value;
        OnCreateScoreText(item.transform.position, item.Value);

        string scoreString = "";
        for (int i = score.ToString().Length; i < 7; i++)
        {
            scoreString += "0";
        }
        scoreString += score.ToString();
        text_Score.text = $"{scoreTranslate}: {scoreString}";
    }

    private void OnCreateScoreText(Vector3 spawnPoint, int value)
    {
        var goScore = Instantiate(textScorePrefab, GameScene.instance.CanvasWorldPosition);
        goScore.transform.position = spawnPoint;
        Text textScore = goScore.GetComponent<Text>();
        textScore.text = $"+{value}";
    }

    private void OnScoreChanged(int value)
    {
        score += value;
        string scoreString = "";
        for (int i = score.ToString().Length; i < 7; i++)
        {
            scoreString += "0";
        }
        scoreString += score.ToString();
        text_Score.text = $"{scoreTranslate}: {scoreString}";
    }

    private void OnLangaugeChanged()
    {
        scoreTranslate = keeper.GetLocalization("GameMenu.UI.Score");
        string scoreString = "";
        for (int i = score.ToString().Length; i < 7; i++)
        {
            scoreString += "0";
        }
        scoreString += score.ToString();
        text_Score.text = $"{scoreTranslate}: {scoreString}";
        text_Score.font = keeper.currentFont;
    }
}
