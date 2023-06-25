using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTarget : UnitCommand
{
    [SerializeField] private UnitCommand _nextStep;
    [Space(10)]
    [SerializeField] private DamageableTag damageableTag;
    [SerializeField] private float damage;

    private MobInfo mobInfo;

    public override UnitCommand NextStep => _nextStep;

    public override void Execute()
    {
        if (mobInfo.AttackTrigger.IsTouching(mobInfo.Mob.targetCollider))
            mobInfo.Mob.targetCollider.GetComponent<IDamageable>().TakeDamage(damage);
    }

    public override void RequestData(MobInfo mobInfo)
    {
        this.mobInfo = mobInfo;
    }
}
