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
        mobInfo.Animator.SetBool("Run", true);

        if (transform.position.x > mobInfo.Mob.target.transform.position.x)
            mobInfo.SpriteRenderer.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        else
            mobInfo.SpriteRenderer.gameObject.transform.localScale = new Vector3(1, 1, 1);

        mobInfo.Agent.SetDestination(mobInfo.Mob.target.position);
    }
}
