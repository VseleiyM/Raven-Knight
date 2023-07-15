using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclesStrike : UnitCommand
{
    public override UnitCommand NextStep => _nextStep;
    [SerializeField] private UnitCommand _nextStep;
    [Space(10)]
    [SerializeField] private Transform jointTent;
    [SerializeField] private Transform tent;
    [SerializeField] private Transform jointStrikeArea;
    [SerializeField] private Transform strikeArea;

    private Vector3 target;
    private Transform temp;

    public override void RequestData(MobInfo mobInfo)
    {
        target = mobInfo.Mob.target.position;
    }

    public override void Execute()
    {
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y, 0f);
        float angle = Mathf.Atan2(target.y - spawnPoint.y, target.x - spawnPoint.x) * Mathf.Rad2Deg;
        float length = (target - spawnPoint).magnitude;

        jointTent.localRotation = Quaternion.Euler(0, 0, angle);
        if (spawnPoint.x < target.x)
            jointTent.localScale = new Vector3(1, 1, 1);
        else
            jointTent.localScale = new Vector3(1, -1, 1);

        jointStrikeArea.localRotation = Quaternion.Euler(0, 0, angle);

        strikeArea.localPosition = Vector3.zero + Vector3.right * length / 2;
        strikeArea.localScale = Vector3.one + Vector3.right * length;
    }
}
