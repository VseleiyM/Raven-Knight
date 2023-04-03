using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackTarget : UnitCommand
{
    [SerializeField] private UnitCommand ifTrue;
    [SerializeField] private UnitCommand ifFalse;

    private Collider2D attacktrigger;
    private Collider2D target;
    private UnitCommand _nextStep;
    private Animator animator;
    private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;

    public override UnitCommand NextStep { get { return _nextStep; } }

    public override void RequestData(MobInfo mobInfo)
    {
        attacktrigger = mobInfo.AttackTrigger;
        target = mobInfo.targetCollider;
        animator = mobInfo.Animator;
        agent = mobInfo.Agent;
        spriteRenderer = mobInfo.SpriteRenderer;
    }

    public override void Execute()
    {
        if (attacktrigger.IsTouching(target))
        {
            _nextStep = ifTrue;

            if (transform.position.x < target.transform.position.x)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;

            animator.SetBool("Attack", true);
            agent.isStopped = true;
        }
        else
        {
            _nextStep = ifFalse;
            animator.SetBool("Attack", false);
            agent.isStopped = false;
        }
    }

}
