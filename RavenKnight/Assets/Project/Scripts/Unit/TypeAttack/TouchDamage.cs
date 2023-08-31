using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDamage : MonoBehaviour
{
    [SerializeField] private DamageableTag damageableTag;
    [SerializeField] private float damage;

    private Collider2D collider2D;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        collider2D.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger) return;

        if (damageableTag != DamageableTag.All
            && !collision.CompareTag(damageableTag.ToString()))
            return;

        collision.GetComponent<IDamageable>().TakeDamage(damage);
    }
}
