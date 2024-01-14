using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class ProjectileSplash : MonoBehaviour
    {
        [SerializeField] private Projectile projectile;
        [SerializeField] private DamageableTag damageableTag;
        [SerializeField] private float damage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.isTrigger) return;
            if (other.tag == DamageableTag.PickupItems.ToString()) return;
            if (damageableTag != DamageableTag.All &&
                other.tag != damageableTag.ToString()) return;

            Target target = other.GetComponent<Target>();
            if (target != null)
                target.TakeDamage(damage);
        }
    }
}