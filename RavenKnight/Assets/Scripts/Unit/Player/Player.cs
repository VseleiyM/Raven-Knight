using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInfo))]

public class Player : MonoBehaviour, IDamageable
{
    public float Speed { get => _speed; }
    [SerializeField] private float _speed = 1;
    public DamageableTag DamageableTag { get => _damageableTag; }
    [SerializeField] private DamageableTag _damageableTag;
    
    private PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }

    public void TakeDamage(float damage)
    {
        playerInfo.Animator.SetTrigger("TakedDamage");
    }
}
