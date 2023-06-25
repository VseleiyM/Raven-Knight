using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : UnitCommand
{
    public override UnitCommand NextStep { get { return _nextStep; } }
    [SerializeField] private UnitCommand _nextStep;

    private NavMeshAgent agent;
    private Transform target;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    public override void RequestData(MobInfo mobInfo)
    {
        agent = mobInfo.Agent;
        target = mobInfo.Mob.target;
        animator = mobInfo.Animator;
        spriteRenderer = mobInfo.SpriteRenderer;
    }

    public override void Execute()
    {
        agent.isStopped = true;
        animator.SetBool("Run", agent.velocity.magnitude > 0);
        spriteRenderer.flipX = transform.position.x > target.transform.position.x;
        agent.SetDestination(target.position);
    }
}
