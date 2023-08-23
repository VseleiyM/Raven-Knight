using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public DamageableTag damageableTag;
    public float damage;
    public CircleCollider2D SplashCollider => _splashCollider;
    [SerializeField] private CircleCollider2D _splashCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger
            || collision.tag == DamageableTag.PickupItems.ToString()) return;

        if (damageableTag != DamageableTag.All &&
            collision.tag != damageableTag.ToString()) return;

        IDamageable target = collision.GetComponent<IDamageable>();
        if (target != null)
            target.TakeDamage(damage);
    }
}
