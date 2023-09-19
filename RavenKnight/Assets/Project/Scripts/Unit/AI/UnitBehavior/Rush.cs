using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : UnitCommand
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

        mobInfo.TargetInfo.Animator.SetBool("Run", false);

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

        curStep = this;
        isMoving = true;

        Vector2 target = mobInfo.target.position;
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
        float curDuration = duration;
        float speed = distance / duration;
        mobInfo.Agent.autoBraking = false;
        while (curDuration > 0)
        {
            curDuration -= Time.fixedDeltaTime;
            Vector2 self = mobInfo.TargetInfo.Rigidbody2D.position;
            Vector2 offset = mobInfo.TargetInfo.Project(direction) * Time.fixedDeltaTime * speed;
            mobInfo.TargetInfo.Rigidbody2D.MovePosition(self + offset);
            yield return new WaitForFixedUpdate();
        }
        mobInfo.Agent.autoBraking = true;
        isFinish = true;
        isMoving = false;
    }
}
