using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEvents : MonoBehaviour
{
    public static event Action<Player> playerInit;
    public static event Action<Player> playerTakeDamage;
    public static event Action<Player> playerDead;
    public static event Action<PickupItem> itemHasPickup;
    public static event Action<int> scoreChanged;
    public static event Action<Vector3,int> createScoreText;
    public static event Action<Mob> mobSpawned;
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

    public static void SendPlayerDead(Player player)
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

    public static void SendMobSpawned(Mob mob)
    {
        mobSpawned?.Invoke(mob);
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
