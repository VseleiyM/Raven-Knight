using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
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
                if (damageableTag == DamageableTag.All
                    || collision.CompareTag(damageableTag.ToString()))
                    collision.GetComponent<Target>().TakeDamage(damage);
            }
        }

        public void AreaEffectDestroy()
        {
            Destroy(gameObject);
        }
    }
}