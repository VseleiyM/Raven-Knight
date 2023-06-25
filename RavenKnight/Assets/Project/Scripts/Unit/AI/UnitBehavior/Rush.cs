using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : UnitCommand
{
    [SerializeField] private UnitCommand _nextStep;
    [Space(10)]
    [SerializeField, Min(1f)] private float speed = 1;
    [SerializeField, Min(0.01f)] private float time = 1;

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
        Vector2 direction = target - self;

        StartCoroutine(MoveRush(direction.normalized));
    }

    public override void RequestData(MobInfo mobInfo)
    {
        this.mobInfo = mobInfo;
    }

    private IEnumerator MoveRush(Vector2 direction)
    {
        float curTime = 0;
        while (curTime < time)
        {
            curTime += Time.deltaTime;
            Vector2 self = mobInfo.transform.position;
            mobInfo.Rigidbody2D.MovePosition(self + direction * Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        isFinish = true;
        isMoving = false;
    }
}
