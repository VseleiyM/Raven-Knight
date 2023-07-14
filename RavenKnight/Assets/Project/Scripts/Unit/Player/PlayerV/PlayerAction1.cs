using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction1 : MonoBehaviour
{
    private PlayerInfo playerInfo;

    private Animator animator;
    private Animator childAnimator;

    private void Awake()
    {
        playerInfo = GetComponentInParent<PlayerInfo>();
        animator = playerInfo.Animator;
        childAnimator = playerInfo.ChildAnimator;
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
        GlobalEvents.SendPlayerDead(playerInfo.Player);
    }
}
