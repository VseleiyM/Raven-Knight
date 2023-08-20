﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public DamageableTag damageableTag;
    public float damage;
    public float speed = 1f;

    public Coroutine coroutine;

    public Animator Animator => _animator;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private TypeAttackInfo attackInfo;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        attackInfo = GetComponent<TypeAttackInfo>();
    }

    private void Start()
    {
        coroutine = StartCoroutine(MoveProjectile());
    }

    public void SoundEffect(AudioClip clip)
    {
        attackInfo.AudioSource.clip = clip;
        attackInfo.AudioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;

        if (coroutine != null)
            StopCoroutine(coroutine);
        _animator.SetTrigger("Hit");

        if (damageableTag != DamageableTag.All
            && !collision.CompareTag(damageableTag.ToString()))
            return;
        
        IDamageable target = collision.GetComponent<IDamageable>();
        if (target != null)
            target.TakeDamage(damage);
    }

    public IEnumerator MoveProjectile()
    {
        while (true)
        {
            Vector2 offset = transform.right * Time.fixedDeltaTime * speed;
            _rigidbody2D.MovePosition(_rigidbody2D.position + offset);
            yield return new WaitForFixedUpdate();
        }

        void Old()
        {
            Vector3 moveVector = transform.position + transform.right * Time.fixedDeltaTime * speed;
            Vector2 offset = new Vector2(moveVector.x, moveVector.y);
            _rigidbody2D.MovePosition(offset);
        }
    }

    public void ProjectileDestroy()
    {
        Destroy(gameObject);
    }
}
