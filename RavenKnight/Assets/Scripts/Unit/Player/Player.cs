using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerInfo playerInfo;

    public void TakeDamage(float damage)
    {
        playerInfo.Animator.SetTrigger("TakedDamage");
    }
}
