using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = GetComponentInParent<PlayerInfo>();
    }

    public void SoundEffect(AudioClip clip)
    {
        playerInfo.TargetInfo.AudioSource.clip = clip;
        playerInfo.TargetInfo.AudioSource.Play();
    }

    public void Dead()
    {
        foreach (var comp in playerInfo.DisableComponents)
            comp.enabled = false;
        GlobalEvents.SendPlayerDead(playerInfo.TargetInfo.Target);
    }
}
