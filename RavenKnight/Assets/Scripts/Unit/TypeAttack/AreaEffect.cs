using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffect : MonoBehaviour
{
    public Collider2D Trigger { get => _trigger; }
    [SerializeField] private Collider2D _trigger;

    public DamageableTag damageableTag;

    public void HitArea()
    {
        List<Collider2D> results = new List<Collider2D>();
        _trigger.GetContacts(results);
        foreach (var collision in results)
        {
            if (damageableTag != DamageableTag.All &&
                collision.tag != damageableTag.ToString())
                break;
            
            collision.GetComponent<IDamageable>().TakeDamage(0f);
        }
        Destroy(gameObject);
    }
}
