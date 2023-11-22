using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DefeatNotification : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GlobalEvents.SendReturnMenu();
        }
    }

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

    private void OnPlayerDead(Target player)
    {
        gameObject.SetActive(true);
    }
}
