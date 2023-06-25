using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UC_Cooldown : UnitCommand
{
    [SerializeField] private UnitCommand ifReady;
    [SerializeField] private UnitCommand IfNot;
    [Space(10)]
    [SerializeField, Min(0)] private float cooldown;

    private bool isReady = true;

    private MobInfo mobInfo;
    private UnitCommand _nextStep;

    public override UnitCommand NextStep => _nextStep;

    public override void Execute()
    {
        if (isReady)
        {
            isReady = false;
            _nextStep = ifReady;
            StartCoroutine(Cooldown());
        }
        else
        {
            _nextStep = IfNot;
        }
    }

    public override void RequestData(MobInfo mobInfo)
    {
        this.mobInfo = mobInfo;
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        isReady = true;
    }
}
