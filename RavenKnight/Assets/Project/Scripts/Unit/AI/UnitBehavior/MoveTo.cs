using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : UnitCommand
{
    public override UnitCommand NextStep { get { return _nextStep; } }
    [SerializeField] private UnitCommand _nextStep;

    private MobInfo mobInfo;

    public override void RequestData(MobInfo mobInfo)
    {
        this.mobInfo = mobInfo;
    }

    public override void Execute()
    {
        mobInfo.Agent.isStopped = false;
        mobInfo.TargetInfo.Animator.SetBool("Run", true);
        mobInfo.TargetInfo.FlipModel(transform.position.x > mobInfo.target.transform.position.x);
        
        mobInfo.Agent.SetDestination(mobInfo.target.position);
    }
}
