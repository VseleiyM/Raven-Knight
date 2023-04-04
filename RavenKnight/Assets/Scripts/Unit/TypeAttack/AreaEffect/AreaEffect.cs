using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffect : MonoBehaviour
{
    public Collider2D Trigger { get => _trigger; }
    [SerializeField] private Collider2D _trigger;

    public string damageableTag;

    public void HitArea()
    {
        List<Collider2D> results = new List<Collider2D>();
        _trigger.GetContacts(results);
        foreach (var collider in results)
        {
            if (collider.tag == damageableTag)
            {
                collider.GetComponent<IDamageable>().TakeDamage(0f);
                break;
            }
        }
        Destroy(gameObject);
    }
}
