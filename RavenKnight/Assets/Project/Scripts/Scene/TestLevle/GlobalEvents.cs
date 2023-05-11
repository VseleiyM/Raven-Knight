﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEvents : MonoBehaviour
{
    public static event Action<Player> playerInit;
    public static event Action<Player> playerTakeDamage;
    public static event Action<int> scoreChanged;
    public static event Action<Vector3, int> createFlyText;
    public static event Action<Mob> mobDead;
    public static event Action<Mob> bossInit;
    public static event Action<Mob> bossTakeDamage;
    public static event Action<Mob> bossDead;
    public static event Action closeRoom;
    public static event Action openRoom;
    public static event Action waveClear;
    public static event Action<int> nextWave;

    public static void SendPlayerInit(Player player)
    {
        playerInit?.Invoke(player);
    }

    public static void SendPlayerTakeDamage(Player player)
    {
        playerTakeDamage?.Invoke(player);
    }

    public static void SendScoreChanged(int value)
    {
        scoreChanged?.Invoke(value);
    }

    public static void SendCreateScoreText(Vector3 spawnPoint, int value)
    {
        createFlyText?.Invoke(spawnPoint, value);
    }

    public static void SendMobDead(Mob mob)
    {
        mobDead?.Invoke(mob);
    }

    public static void SendBossInit(Mob mob)
    {
        bossInit?.Invoke(mob);
    }

    public static void SendBossTakeDamage(Mob mob)
    {
        bossTakeDamage?.Invoke(mob);
    }

    public static void SendBossDead(Mob mob)
    {
        bossDead?.Invoke(mob);
    }

    public static void SendCloseRoom()
    {
        closeRoom?.Invoke();
    }

    public static void SendOpenRoom()
    {
        openRoom?.Invoke();
    }

    public static void SendWaveClear()
    {
        waveClear?.Invoke();
    }

    public static void SendNextWave(int index)
    {
        nextWave?.Invoke(index);
    }
}
