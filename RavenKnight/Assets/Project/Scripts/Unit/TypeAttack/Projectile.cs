﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    public DamageableTag damageableTag;
    public float damage;

    public Coroutine coroutine;

    private Animator animator;
    private Rigidbody2D _rigidbody;
    private TypeAttackInfo attackInfo;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        attackInfo = GetComponent<TypeAttackInfo>();
    }

    private void Start()
    {
        coroutine = StartCoroutine(MoveRight());
    }

    public void SoundEffect(string nameAudio)
    {
        foreach (var clip in attackInfo.ListAudioClip)
        {
            if (clip.name.ToLower() == nameAudio)
            {
                attackInfo.AudioSource.clip = clip;
                attackInfo.AudioSource.Play();
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;
        if (collision.tag == DamageableTag.PickupItems.ToString()) return;

        StopCoroutine(coroutine);
        animator.SetTrigger("Hit");

        if (damageableTag != DamageableTag.All &&
            collision.tag != damageableTag.ToString())
            return;
        
        IDamageable target = collision.GetComponent<IDamageable>();
        if (target != null)
            target.TakeDamage(damage);
    }

    public IEnumerator MoveRight()
    {
        while (true)
        {
            Vector3 moveVector = transform.position + transform.right * Time.fixedDeltaTime * speed;
            Vector2 offset = new Vector2(moveVector.x, moveVector.y);
            _rigidbody.MovePosition(offset);
            yield return new WaitForFixedUpdate();
        }
    }

    public void ProjectileDestroy()
    {
        Destroy(gameObject);
    }
}
