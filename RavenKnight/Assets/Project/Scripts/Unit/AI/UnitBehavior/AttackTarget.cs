using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackTarget : UnitCommand
{
    [SerializeField] private UnitCommand ifTrue;
    [SerializeField] private UnitCommand ifFalse;
    [Space(10)]
    [SerializeField, Min(1)] private int attackVariant = 1;
    [SerializeField] private bool lookAtTarget;

    private bool attack = false;
    private bool attackReady = true;

    private MobInfo mobInfo;
    private MobAction mobAction;
    private UnitCommand _nextStep;

    public override UnitCommand NextStep { get { return _nextStep; } }

    private void Awake()
    {
        mobAction = GetComponentInParent<MobAction>();
        mobAction.attack += OnAttack;
        mobAction.attackFinished += OnAttackFinish;
    }

    private void OnDestroy()
    {
        mobAction.attack -= OnAttack;
        mobAction.attackFinished -= OnAttackFinish;
    }

    public override void RequestData(MobInfo mobInfo)
    {
        this.mobInfo = mobInfo;
    }

    public override void Execute()
    {
        if (!attackReady)
        {
            if (attack)
            {
                _nextStep = ifTrue;
                attack = false;
                mobInfo.TargetInfo.Animator.SetBool("Attack", false);
            }
            else
            {
                _nextStep = this;
            }
            return;
        }

        if (mobInfo.AttackTrigger.IsTouching(mobInfo.targetCollider))
        {
            attackReady = false;
            _nextStep = this;
            Vector3 newScale = mobInfo.TargetInfo.SpriteRenderer.gameObject.transform.localScale;
            if (lookAtTarget)
                if (transform.position.x > mobInfo.target.transform.position.x)
                {
                    newScale = new Vector3 (Mathf.Abs(newScale.x) * -1, newScale.y, newScale.z);
                    mobInfo.TargetInfo.SpriteRenderer.gameObject.transform.localScale = newScale;
                }
                else
                {
                    newScale = new Vector3(Mathf.Abs(newScale.x), newScale.y, newScale.z);
                    mobInfo.TargetInfo.SpriteRenderer.gameObject.transform.localScale = newScale;
                }

            mobInfo.TargetInfo.Animator.SetInteger("AttackVariant", attackVariant);
            mobInfo.TargetInfo.Animator.SetBool("Attack", true);
            mobInfo.TargetInfo.Animator.SetBool("Run", false);
            mobInfo.Agent.SetDestination(this.transform.position);
        }
        else
        {
            _nextStep = ifFalse;
            mobInfo.TargetInfo.Animator.SetBool("Attack", false);
            mobInfo.Agent.isStopped = false;
        }
    }

    private void OnAttack(int variant)
    {
        if (variant == attackVariant)
            attack = true;
    }

    private void OnAttackFinish(int variant)
    {
        if (variant == attackVariant)
        {
            attackReady = true;
            attack = false;
        }
    }
}
