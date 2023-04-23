using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInfo))]

public class MouseInput : MonoBehaviour
{
    private PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            playerInfo.weapon.Shoot();
        }
    }
}
