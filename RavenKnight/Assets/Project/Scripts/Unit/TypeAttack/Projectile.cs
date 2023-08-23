using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public DamageableTag damageableTag;
    public float damage;
    public float speed = 1f;
    [Min(0)] public float splashRadius = 0;
    public float splashDamage;

    public Coroutine coroutine;

    public Animator Animator => _animator;
    private Animator _animator;
    public TypeAttackInfo AttackInfo => _attackInfo;
    private TypeAttackInfo _attackInfo;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _attackInfo = GetComponent<TypeAttackInfo>();
    }

    private void Start()
    {
        coroutine = StartCoroutine(MoveProjectile());
    }

    public void SoundEffect(AudioClip clip)
    {
        _attackInfo.AudioSource.clip = clip;
        _attackInfo.AudioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;

        if (coroutine != null)
            StopCoroutine(coroutine);

        _animator.SetTrigger(AnimatorParameter.Hit.ToString());
        if (_attackInfo.Splash != null
            && splashRadius > 0.0001)
        {
            _attackInfo.Splash.SplashCollider.radius = splashRadius;
            _attackInfo.Splash.damageableTag = damageableTag;
            _attackInfo.Splash.damage = splashDamage;
            _animator.SetTrigger(AnimatorParameter.ProjectileSplash.ToString());
        }

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
