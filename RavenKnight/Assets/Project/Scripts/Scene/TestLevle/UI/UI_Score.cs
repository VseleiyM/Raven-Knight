using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Score : MonoBehaviour
{
    [SerializeField] private Text text_Score;

    private void OnEnable()
    {
        GlobalEvents.playerInit += OnPlayerInit;
        GlobalEvents.playerScoreChanged += OnPlayerScoreChanged;
    }

    private void OnDisable()
    {
        GlobalEvents.playerInit -= OnPlayerInit;
        GlobalEvents.playerScoreChanged -= OnPlayerScoreChanged;
    }

    private void OnPlayerInit(Player player)
    {
        string scoreString = "";
        for (int i = player.Score.ToString().Length; i < 7; i++)
        {
            scoreString += "0";
        }
        scoreString += player.Score.ToString();
        text_Score.text = $"Score: {scoreString}";
    }

    private void OnPlayerScoreChanged(Player player)
    {
        string scoreString = "";
        for (int i = player.Score.ToString().Length; i < 7; i++)
        {
            scoreString += "0";
        }
        scoreString += player.Score.ToString();
        text_Score.text = $"Score: {scoreString}";
    }
}
