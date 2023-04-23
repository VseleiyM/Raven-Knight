using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UC_Wait : UnitCommand
{
    public override UnitCommand NextStep => _result;
    private UnitCommand _result;

    [SerializeField] private UnitCommand _nextStep;
    [Space(10)]
    [Min(0)]
    [SerializeField] private float time;

    private bool isReady = true;
    private bool isWait = false;

    public override void RequestData(MobInfo mobInfo)
    {
        
    }

    public override void Execute()
    {
        if (isReady)
        {
            _result = this;
            isWait = true;
            isReady = false;
            StartCoroutine(Wait());
        }
        else
        {
            if (!isWait)
            {
                _result = _nextStep;
                isReady = true;
            }
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(time);
        isWait = false;
    }
}
