﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject gate;

    private void OnEnable()
    {
        GlobalEvents.closeRoom += OnRoomClose;
        GlobalEvents.openRoom += OnRoomOpen;
    }

    private void OnDisable()
    {
        GlobalEvents.closeRoom -= OnRoomClose;
        GlobalEvents.openRoom -= OnRoomOpen;
    }

    private void OnRoomClose()
    {
        gate.SetActive(true);
    }

    private void OnRoomOpen()
    {
        gate.SetActive(false);
    }
}
