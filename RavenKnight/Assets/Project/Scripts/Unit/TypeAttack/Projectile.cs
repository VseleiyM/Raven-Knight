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
    [SerializeField] private AnimationCurve fluctuation;
    [SerializeField, Min(0.1f)] private float durationFluctuation = 1f;

    public Animator Animator => _animator;
    private Animator _animator;
    public TypeAttackInfo AttackInfo => _attackInfo;
    private TypeAttackInfo _attackInfo;
    private Rigidbody2D _rigidbody2D;

    public Coroutine coroutine;

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
        float expiredTime = 0;
        float progress = 0;
        Vector2 currentPosition = transform.position;

        while (true)
        {
            expiredTime += Time.fixedDeltaTime;

            if (expiredTime > durationFluctuation)
                expiredTime = 0;

            progress = expiredTime / durationFluctuation;

            Vector2 offsetRight = transform.right * Time.fixedDeltaTime * speed;
            currentPosition += offsetRight;
            Vector2 offsetUp = transform.up * fluctuation.Evaluate(progress);
            _rigidbody2D.MovePosition(currentPosition + offsetUp);
            yield return new WaitForFixedUpdate();
        }
    }

    public void ProjectileDestroy()
    {
        Destroy(gameObject);
    }
}
