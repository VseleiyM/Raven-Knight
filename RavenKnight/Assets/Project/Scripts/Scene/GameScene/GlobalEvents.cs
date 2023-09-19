using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEvents
{
    public static event Action<Target> playerInit;
    public static event Action<Target, string> targetTakeDamage;
    public static event Action<Target> playerDead;
    public static event Action<PickupItem> itemHasPickup;
    public static event Action<int> scoreChanged;
    public static event Action<Vector3,int> createScoreText;
    public static event Action<Target> mobSpawned;
    public static event Action<Target> mobDead;
    public static event Action<Target> bossInit;
    public static event Action<Target> bossTakeDamage;
    public static event Action<Target> bossDead;
    public static event Action closeRoom;
    public static event Action<EnemySpawner> openRoom;
    public static event Action waveClear;
    public static event Action bossRoomClear;
    public static event Action<int, bool> nextWave;
    public static event Action<bool> pauseStatus;
    public static event Action returnMenu;

    public static void SendPlayerInit(Target player)
    {
        playerInit?.Invoke(player);
    }

    public static void SendTargetTakeDamage(Target target, string tag)
    {
        targetTakeDamage?.Invoke(target, tag);
    }

    public static void SendPlayerDead(Target player)
    {
        playerDead?.Invoke(player);
    }

    public static void SendItemHasPickup(PickupItem item)
    {
        itemHasPickup?.Invoke(item);
    }

    public static void SendScoreChanged(int score)
    {
        scoreChanged?.Invoke(score);
    }

    public static void SendCreateScoreText(Vector3 spawnPoint, int score)
    {
        createScoreText?.Invoke(spawnPoint, score);
    }

    public static void SendMobSpawned(Target target)
    {
        mobSpawned?.Invoke(target);
    }

    public static void SendMobDead(Target target)
    {
        mobDead?.Invoke(target);
    }

    public static void SendBossInit(Target target)
    {
        bossInit?.Invoke(target);
    }

    public static void SendBossTakeDamage(Target target)
    {
        bossTakeDamage?.Invoke(target);
    }

    public static void SendBossDead(Target target)
    {
        bossDead?.Invoke(target);
    }

    public static void SendCloseRoom()
    {
        closeRoom?.Invoke();
    }

    public static void SendOpenRoom(EnemySpawner spawner)
    {
        openRoom?.Invoke(spawner);
    }

    public static void SendWaveClear()
    {
        waveClear?.Invoke();
    }

    public static void SendBossRoomClear()
    {
        bossRoomClear?.Invoke();
    }

    public static void SendNextWave(int index, bool bossRoom)
    {
        nextWave?.Invoke(index, bossRoom);
    }

    public static void SendPauseStatus(bool status)
    {
        pauseStatus?.Invoke(status);
    }

    public static void SendReturnMenu()
    {
        returnMenu?.Invoke();
    }
}
