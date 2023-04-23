using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInfo))]

public class Player : MonoBehaviour, IDamageable
{
    public float CurrentHealth { get => _currentHealth; }
    [SerializeField] private float _currentHealth;
    public float MaxHealth { get => _maxHealth; }
    [SerializeField] private float _maxHealth;
    public float Speed { get => _speed; }
    [SerializeField] private float _speed = 1;
    public DamageableTag DamageableTag { get => _damageableTag; }
    [SerializeField] private DamageableTag _damageableTag;

    public bool IsDead { get => _isDead; }
    [SerializeField] private bool _isDead;
    public bool IsInvincible { get => _isInvincible; }
    [SerializeField] private bool _isInvincible;
    public List<MonoBehaviour> DisableComponents { get => _disableComponents; }
    [SerializeField] private List<MonoBehaviour> _disableComponents;

    private PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();

        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_isInvincible) return;

        if (!_isDead)
        {
            _currentHealth = _currentHealth - damage;

            if (_currentHealth > 0)
            {
                playerInfo.Animator.SetTrigger("TakedDamage");
            }
            else
            {
                playerInfo.Animator.SetTrigger("Dead");
                _isDead = true;
            }
        }
    }
}
