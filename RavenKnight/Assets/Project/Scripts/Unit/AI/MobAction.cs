using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobAction : MonoBehaviour
{
    private MobInfo mobInfo;
    private Transform folder;

    public event Action takeDamage;
    public event Action<int> attack;
    public event Action<int> attackFinished;

    private void Awake()
    {
        var goFolder = GameObject.Find("Temp");
        if (!goFolder)
            folder = new GameObject("Temp").transform;
        else
            folder = goFolder.transform;

        mobInfo = GetComponentInParent<MobInfo>();
    }

    public void SendMobDead()
    {
        GlobalEvents.SendMobDead(mobInfo.Mob);
        GlobalEvents.SendScoreChanged(mobInfo.Mob.GainScore);
        GlobalEvents.SendCreateScoreText(mobInfo.transform.position, mobInfo.Mob.GainScore);
    }

    public void DestroyUnit()
    {
        Destroy(mobInfo.gameObject);
    }

    public void SoundEffect(AudioClip clip)
    {
        if (clip == null) return;

        mobInfo.AudioSource.clip = clip;
        mobInfo.AudioSource.Play();
    }

    public void SendTakeDamage()
    {
        takeDamage?.Invoke();
    }

    public void AttackVariant(int variant)
    {
        attack?.Invoke(variant);
    }

    public void AttackFinished(int variant)
    {
        attackFinished?.Invoke(variant);
    }

    public void MobSpawned()
    {
        mobInfo.PhysicsCollider.enabled = true;
        mobInfo.Mob.EnableAI();
    }
}
