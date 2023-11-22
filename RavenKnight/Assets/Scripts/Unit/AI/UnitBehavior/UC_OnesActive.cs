using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UC_OnesActive : UnitCommand
{
    [SerializeField] private UnitCommand ifTrue;
    [SerializeField] private UnitCommand ifFalse;
    [Space(10)]
    [SerializeField] private bool triggered;

    private UnitCommand _nextStep;
    private MobInfo mobInfo;

    public override UnitCommand NextStep => _nextStep;

    public override void Execute()
    {
        if (!triggered)
            _nextStep = ifTrue;
        else
            _nextStep = ifFalse;
    }

    public override void RequestData(MobInfo mobInfo)
    {
        this.mobInfo = mobInfo;
    }
}
