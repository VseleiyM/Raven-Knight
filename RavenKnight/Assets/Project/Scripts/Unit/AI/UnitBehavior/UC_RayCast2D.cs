using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UC_RayCast2D : UnitCommand
{
    [SerializeField] private UnitCommand ifTrue;
    [SerializeField] private UnitCommand ifFalse;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private DamageableTag damageableTag;

    private MobInfo mobInfo;

    public override UnitCommand NextStep => _nextStep;
    private UnitCommand _nextStep;

    public override void Execute()
    {
        Vector2 self = mobInfo.transform.position;
        Vector2 target = mobInfo.Mob.target.transform.position;
        Vector2 direction = target - self;

        RaycastHit2D hit = Physics2D.Raycast(self, direction.normalized, 100f, layerMask);

        if (hit)
        {
            if (hit.collider.gameObject.layer != (int)LayerName.Level
                && (hit.collider.tag == damageableTag.ToString()
                || damageableTag == DamageableTag.All))
            {
                _nextStep = ifTrue;
            }
            else
            {
                _nextStep = ifFalse;
            }
        }
        else
        {
            _nextStep = ifFalse;
        }
    }

    public override void RequestData(MobInfo mobInfo)
    {
        this.mobInfo = mobInfo;
    }
}
