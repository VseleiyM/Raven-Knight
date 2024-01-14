﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private GameObject gate;

        private void Start()
        {
            gate.SetActive(false);
        }

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

        private void OnRoomOpen(int score)
        {
            gate.SetActive(false);
        }
    }
}