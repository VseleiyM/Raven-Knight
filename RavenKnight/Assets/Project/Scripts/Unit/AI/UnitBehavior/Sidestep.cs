﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidestep : UnitCommand
{
    [SerializeField] private UnitCommand _nextStep;
    [Space(10)]
    [SerializeField, Min(0.01f)] private float distance = 1;
    [SerializeField, Min(0.01f)] private float duration = 1;

    private MobInfo mobInfo;
    private UnitCommand curStep;
    private bool isMoving = false;
    private bool isFinish = false;

    public override UnitCommand NextStep => curStep;

    public override void Execute()
    {
        if (isFinish)
        {
            curStep = _nextStep;
            isFinish = false;
            return;
        }

        if (isMoving) return;

        mobInfo.Agent.isStopped = true;
        mobInfo.Animator.SetBool("Run", false);
        curStep = this;
        isMoving = true;

        Vector2 target = mobInfo.Mob.target.position;
        Vector2 self = mobInfo.transform.position;
        Vector2 ray = self - target;
        Vector2 direction;
        if (Random.Range(0, 2) == 0)
            direction = new Vector2(-ray.y, ray.x);
        else
            direction = new Vector2(ray.y, -ray.x);
        
        StartCoroutine(MoveSidestep(direction.normalized));
    }

    public override void RequestData(MobInfo mobInfo)
    {
        this.mobInfo = mobInfo;
    }

    private IEnumerator MoveSidestep(Vector2 direction)
    {
        float curDuration = duration;
        mobInfo.Agent.enabled = false;
        float speed = distance / duration;
        while (curDuration > 0)
        {
            curDuration -= Time.fixedDeltaTime;
            Vector2 self = mobInfo.Rigidbody2D.position;
            Vector2 offset = mobInfo.Project(direction) * Time.fixedDeltaTime * speed;
            mobInfo.Rigidbody2D.MovePosition(self + offset);
            yield return new WaitForFixedUpdate();
        }
        mobInfo.Agent.enabled = true;
        isFinish = true;
        isMoving = false;
    }
}
