using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : UnitCommand
{
    [SerializeField] private UnitCommand _nextStep;
    [Space(10)]
    [SerializeField, Min(1f)] private float distance = 1;
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
        if (transform.position.x > mobInfo.Mob.target.transform.position.x)
            mobInfo.SpriteRenderer.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        else
            mobInfo.SpriteRenderer.gameObject.transform.localScale = new Vector3(1, 1, 1);
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
        float speed = distance / time;
        mobInfo.Agent.enabled = false;
        while (curTime < time)
        {
            curTime += Time.fixedDeltaTime;
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
