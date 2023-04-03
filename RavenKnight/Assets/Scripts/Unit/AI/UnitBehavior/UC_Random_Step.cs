using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UC_Random_Step : UnitCommand
{
    public override UnitCommand NextStep => _nextStep;
    private UnitCommand _nextStep;

    [SerializeField] private List<UnitCommand> commandsList;

    public override void RequestData(MobInfo mobInfo)
    {
        
    }

    public override void Execute()
    {
        _nextStep = commandsList[Random.Range(0, commandsList.Count)];
    }
}
