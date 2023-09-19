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
        Vector3 newScale = mobInfo.TargetInfo.SpriteRenderer.gameObject.transform.localScale;
        if (transform.position.x > mobInfo.target.transform.position.x)
        {
            newScale = new Vector3(Mathf.Abs(newScale.x) * -1, newScale.y, newScale.z);
            mobInfo.TargetInfo.SpriteRenderer.gameObject.transform.localScale = newScale;
        }
        else
        {
            newScale = new Vector3(Mathf.Abs(newScale.x), newScale.y, newScale.z);
            mobInfo.TargetInfo.SpriteRenderer.gameObject.transform.localScale = newScale;
        }

        mobInfo.Agent.SetDestination(mobInfo.target.position);
    }
}
