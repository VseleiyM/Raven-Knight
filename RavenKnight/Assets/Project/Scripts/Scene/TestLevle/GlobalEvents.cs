using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEvents : MonoBehaviour
{
    public static event Action<Player> playerInit;
    public static event Action<Player> playerTakeDamage;
    public static event Action<Player> playerScoreChanged;
    public static event Action<Mob> mobDead;
    public static event Action<Mob> bossInit;
    public static event Action<Mob> bossTakeDamage;
    public static event Action<Mob> bossDead;

    public static void SendPlayerInit(Player player)
    {
        playerInit.Invoke(player);
    }

    public static void SendPlayerTakeDamage(Player player)
    {
        playerTakeDamage.Invoke(player);
    }

    public static void SendPlayerScoreChanged(Player player)
    {
        playerScoreChanged.Invoke(player);
    }

    public static void SendMobDead(Mob mob)
    {
        mobDead.Invoke(mob);
    }

    public static void SendBossInit(Mob mob)
    {
        bossInit.Invoke(mob);
    }

    public static void SendBossTakeDamage(Mob mob)
    {
        bossTakeDamage.Invoke(mob);
    }

    public static void SendBossDead(Mob mob)
    {
        bossDead.Invoke(mob);
    }
}
