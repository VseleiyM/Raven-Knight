using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_Score : MonoBehaviour
{
    [SerializeField] private TypePickupItem pickupItem;
    [SerializeField] private GameObject textScorePrefab;
    [SerializeField] private Text text_Score;

    private int score = 0;

    private void OnEnable()
    {
        GlobalEvents.itemHasPickup += OnItemHasPickup;
        GlobalEvents.scoreChanged += OnScoreChanged;
        GlobalEvents.createScoreText += OnCreateScoreText;
    }

    private void OnDisable()
    {
        GlobalEvents.itemHasPickup -= OnItemHasPickup;
        GlobalEvents.scoreChanged -= OnScoreChanged;
        GlobalEvents.createScoreText -= OnCreateScoreText;
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
        text_Score.text = $"Score: {scoreString}";
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
        text_Score.text = $"Score: {scoreString}";
    }
}
