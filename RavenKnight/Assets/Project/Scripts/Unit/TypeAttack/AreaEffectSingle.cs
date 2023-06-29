using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectSingle : MonoBehaviour
{
    public Collider2D Trigger { get => _trigger; }
    [SerializeField] private Collider2D _trigger;

    public DamageableTag damageableTag;
    public float damage;

    public void HitArea()
    {
        List<Collider2D> results = new List<Collider2D>();
        _trigger.GetContacts(results);
        foreach (var collision in results)
        {
            if (collision.tag == damageableTag.ToString())
            {
                collision.GetComponent<IDamageable>().TakeDamage(damage);
                break;
            }

            if (damageableTag == DamageableTag.All)
            {
                collision.GetComponent<IDamageable>().TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
