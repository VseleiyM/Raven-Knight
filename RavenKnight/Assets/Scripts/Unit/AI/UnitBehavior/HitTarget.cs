using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class HitTarget : UnitCommand
    {
        [SerializeField] private UnitCommand _nextStep;
        [Space(10)]
        [SerializeField] private DamageableTag damageableTag;
        [SerializeField] private float damage;
        [SerializeField] private Collider2D attackTrigger;
        [SerializeField] private bool triggerEnter;

        private MobInfo mobInfo;

        public override UnitCommand NextStep => _nextStep;

        public override void Execute()
        {
            if (triggerEnter) return;

            Collider2D trigger;
            Collider2D target = mobInfo.targetCollider;

            if (attackTrigger)
                trigger = attackTrigger;
            else
                trigger = mobInfo.AttackTrigger;

            if (trigger.IsTouching(target))
                target.GetComponent<Target>().TakeDamage(damage);
        }

        public override void RequestData(MobInfo mobInfo)
        {
            this.mobInfo = mobInfo;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!triggerEnter) return;
            if (collision.isTrigger) return;

            if (collision.tag == damageableTag.ToString())
            {
                collision.GetComponent<Target>().TakeDamage(damage);
            }
        }
    }
}