﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclesStrike : UnitCommand
{
    public override UnitCommand NextStep => _nextStep;
    [SerializeField] private UnitCommand _nextStep;

    [Min(0)]
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private GameObject areaAttack;
    [SerializeField] private DamageableTag damageableTag;

    private bool isReady = true;
    private Vector3 target;
    private Transform temp;

    private void Awake()
    {
        var goTemp = GameObject.Find("Temp");
        if (!goTemp)
            temp = new GameObject("Temp").transform;
        else
            temp = goTemp.transform;
    }

    public override void RequestData(MobInfo mobInfo)
    {
        target = mobInfo.target.position;
    }

    public override void Execute()
    {
        if (!isReady) return;
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y, 0f);
        float angle = Mathf.Atan2(target.y - spawnPoint.y, target.x - spawnPoint.x) * Mathf.Rad2Deg;
        float length = (target - spawnPoint).magnitude;

        var area = Instantiate(areaAttack, spawnPoint, Quaternion.Euler(0f, 0f, angle));
        area.transform.parent = temp;
        AreaEffect compAreaEffect = area.GetComponent<AreaEffect>();
        compAreaEffect.damageableTag = damageableTag;
        compAreaEffect.Trigger.gameObject.layer = gameObject.layer;

        compAreaEffect.Trigger.transform.localPosition = compAreaEffect.Trigger.transform.localPosition + Vector3.right * length / 2;
        compAreaEffect.Trigger.transform.localScale = compAreaEffect.Trigger.transform.localScale + Vector3.right * length;

        isReady = false;
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        isReady = true;
    }
}
