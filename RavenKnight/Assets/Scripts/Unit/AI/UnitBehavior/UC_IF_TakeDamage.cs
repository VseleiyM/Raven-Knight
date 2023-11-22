using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UC_IF_TakeDamage : UnitCommand
{
    [SerializeField] private UnitCommand ifTrue;
    [SerializeField] private UnitCommand ifFalse;

    private bool isTakeDamage = false;

    private MobInfo mobInfo;
    private MobAction mobAction;
    private UnitCommand _nextStep;

    private void Awake()
    {
        mobAction = GetComponentInParent<MobAction>();
        mobAction.takeDamage += OnMobTakeDamage;
    }

    private void OnDestroy()
    {
        mobAction.takeDamage -= OnMobTakeDamage;
    }

    public override UnitCommand NextStep => _nextStep;

    public override void Execute()
    {
        if (isTakeDamage)
        {
            _nextStep = ifTrue;
            isTakeDamage = false;
        }
        else
            _nextStep = ifFalse;
    }

    public override void RequestData(MobInfo mobInfo)
    {
        this.mobInfo = mobInfo;
    }

    private void OnMobTakeDamage()
    {
        isTakeDamage = true;
    }
}
