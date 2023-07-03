using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DefeatNotification : MonoBehaviour
{
    private void Awake()
    {
        GlobalEvents.playerDead += OnPlayerDead;
    }

    private void OnDestroy()
    {
        GlobalEvents.playerDead -= OnPlayerDead;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnPlayerDead(Player player)
    {
        gameObject.SetActive(true);
    }
}
