using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Score : MonoBehaviour
{
    [SerializeField] private GameObject textScorePrefab;
    [SerializeField] private Text text_Score;

    private int score = 0;

    private void OnEnable()
    {
        GlobalEvents.scoreChanged += OnPlayerScoreChanged;
        GlobalEvents.createFlyText += OnCreateScoreText;
    }

    private void OnDisable()
    {
        GlobalEvents.scoreChanged -= OnPlayerScoreChanged;
        GlobalEvents.createFlyText -= OnCreateScoreText;
    }

    private void OnPlayerScoreChanged(int value)
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

    private void OnCreateScoreText(Vector3 spawnPoint, int value)
    {
        var goScore = Instantiate(textScorePrefab, GameScene.instance.CanvasWorldPosition);
        goScore.transform.position = spawnPoint;
        Text textScore = goScore.GetComponent<Text>();
        textScore.text = $"+{value}";
    }
}
