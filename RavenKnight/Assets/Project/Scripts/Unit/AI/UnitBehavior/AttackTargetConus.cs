using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackTargetConus : UnitCommand
{
    [SerializeField] private UnitCommand ifTrue;
    [SerializeField] private UnitCommand ifFalse;

    [SerializeField] private int projectileCount;
    [SerializeField] private float corner;

    private Collider2D attacktrigger;
    private Collider2D target;
    private UnitCommand _nextStep;
    private Animator animator;
    private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;

    public override UnitCommand NextStep => _nextStep;
    public override void RequestData(MobInfo mobInfo)
    {
    }

    public override void Execute()
    {
        if (attacktrigger.IsTouching(target))
        {
            _nextStep = ifTrue;

        }
        else
        {
            _nextStep = ifFalse;

        }
    }

}
