using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_VictoryNotification : MonoBehaviour
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
        GlobalEvents.bossRoomClear += OnBossRoomClear;
    }

    private void OnDestroy()
    {
        GlobalEvents.bossRoomClear -= OnBossRoomClear;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnBossRoomClear()
    {
        gameObject.SetActive(true);
    }
}
