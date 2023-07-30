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
    [SerializeField, Min(0.01f)] private float _invincibleDuration = 0.2f;

    public bool IsDead { get => _isDead; }
    [Space(10)] [SerializeField] private bool _isDead;
    public List<MonoBehaviour> DisableComponents { get => _disableComponents; }
    [SerializeField] private List<MonoBehaviour> _disableComponents;
    public PlayerInfo PlayerInfo => _playerInfo;
    private PlayerInfo _playerInfo;

    private bool _offTakeDamage;
    private Coroutine takeDamageEffect;
    private Coroutine offCollider;

    private void Awake()
    {
        _playerInfo = GetComponent<PlayerInfo>();
        _currentHealth = _maxHealth;
        GlobalEvents.bossRoomClear += OnBossRoomClear;
    }

    private void Start()
    {
        GlobalEvents.SendPlayerInit(this);
    }

    private void OnDestroy()
    {
        GlobalEvents.bossRoomClear -= OnBossRoomClear;
    }

    public void ChangeInvincible()
    {
        _offTakeDamage = !_offTakeDamage;
    }

    private void OnBossRoomClear()
    {
        foreach (var comp in _disableComponents)
            comp.enabled = false;
    }

    public void TakeDamage(float damage)
    {
        if (_offTakeDamage) return;

        if (!_isDead)
        {
            _currentHealth -= damage;

            if (_currentHealth > _maxHealth)
                _currentHealth = _maxHealth;

            GlobalEvents.SendPlayerTakeDamage(this);

            if (_currentHealth > 0)
            {
                if (damage > 0)
                {
                    if (takeDamageEffect != null)
                        StopCoroutine(takeDamageEffect);
                    takeDamageEffect = StartCoroutine(TakeDamageEffect());

                    ActiveInvincibleEffect(_invincibleDuration);
                }
                else if (damage < 0)
                {
                    _playerInfo.Animator.SetTrigger(AnimatorParameter.TakedHeal.ToString());
                }
            }
            else
            {
                _playerInfo.Animator.SetTrigger(AnimatorParameter.Dead.ToString());
                _isDead = true;
            }
        }
    }

    private IEnumerator TakeDamageEffect()
    {
        float takeDamage = 1;
        while (takeDamage > 0)
        {
            _playerInfo.SpriteRenderer.material.SetFloat("_TakeDamage", takeDamage);
            takeDamage -= Time.deltaTime * 4;
            yield return new WaitForEndOfFrame();
        }
    }

    public void ActiveInvincibleEffect(float duration)
    {
        if (offCollider != null)
            StopCoroutine(offCollider);

        offCollider = StartCoroutine(OffCollider(duration));
    }

    private IEnumerator OffCollider(float duration)
    {
        _playerInfo.PhysycCollider.enabled = false;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _playerInfo.PhysycCollider.enabled = true;
    }
}
