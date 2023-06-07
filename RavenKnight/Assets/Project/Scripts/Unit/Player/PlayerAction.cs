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
        playerInfo.AudioSource.clip = clip;
        playerInfo.AudioSource.Play();
    }

    public void Dead()
    {
        foreach (var comp in playerInfo.Player.DisableComponents)
            comp.enabled = false;
    }
}
