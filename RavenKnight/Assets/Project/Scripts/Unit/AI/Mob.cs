﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour, IDamageable
{
    public int GainScore => _gainScore;
    [SerializeField] private int _gainScore;
    public bool IsBoss => _isBoss;
    [SerializeField] private bool _isBoss;

    [Space(10)]
    [SerializeField] private List<UnitCommand> startStep;
    [SerializeField] private List<DropSlot> dropList;
    public List<UnitParameter> Parameters => _parameters;
    [SerializeField] private List<UnitParameter> _parameters;


    [Space(10)]
    public Transform target;
    public Collider2D targetCollider;
    public Coroutine corotine_AI;

    private Coroutine corTakeDamage;
    private MobInfo mobInfo;
    private bool enableAI = false;
    private MobAction mobAction;
    private Transform folder;

    private void Awake()
    {
        var goFolder = GameObject.Find("Temp");
        if (!goFolder)
            folder = new GameObject("Temp").transform;
        else
            folder = goFolder.transform;

        foreach (var parameter in _parameters)
        {
            if (parameter.FillAwake)
                parameter.current = parameter.Max;
        }

        mobAction = GetComponentInChildren<MobAction>();
        GlobalEvents.SendMobSpawned(this);
    }

    private void Start()
    {
        mobInfo = GetComponent<MobInfo>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetCollider = target.GetComponent<Collider2D>();
        GlobalEvents.playerDead += OnPlayerDead;

        if (IsBoss)
            GlobalEvents.SendBossInit(this);
    }

    private void OnDestroy()
    {
        GlobalEvents.playerDead -= OnPlayerDead;
    }

    private void FixedUpdate()
    {
        if (!enableAI) return;

        for (int i = 0; i < startStep.Count; i++)
        {
            startStep[i].RequestData(mobInfo);
            startStep[i].Execute();
            startStep[i] = startStep[i].NextStep;
        }
    }

    public void EnableAI()
    {
        enableAI = true;
        mobInfo.Agent.isStopped = false;
    }

    public void OnPlayerDead(Player player)
    {
        enableAI = false;
        mobInfo.Animator.SetBool(AnimatorParameter.Run.ToString(), false);
        mobInfo.Animator.SetBool(AnimatorParameter.Attack.ToString(), false);
        if (!mobInfo.Agent.isStopped)
            mobInfo.Agent.isStopped = true;
    }

    public void TakeDamage(float damage)
    {
        UnitParameter healthParameter = null;
        foreach (var parameter in Parameters)
        {
            if (parameter.Parameter == ParametersList.Health)
            {
                healthParameter = parameter;
                break;
            }
        }

        if (IsBoss) GlobalEvents.SendBossTakeDamage(this);
        if (mobAction) mobAction.SendTakeDamage();

        if (healthParameter == null) return;

        healthParameter.current -= damage;
        if (healthParameter.current > 0)
        {
            if (damage > 0)
            {
                if (corTakeDamage != null)
                    StopCoroutine(corTakeDamage);

                corTakeDamage = StartCoroutine(CorTakeDamage());
            }
        }
        else
        {
            StopAllCoroutines();
            mobInfo.Agent.enabled = true;
            mobInfo.Agent.isStopped = true;
            mobInfo.PhysicsCollider.enabled = false;
            DropItemLogic();
            mobInfo.Animator.SetTrigger("Dead");

            void DropItemLogic()
            {
                foreach (var dropSlot in dropList)
                {
                    if (!dropSlot.DropItemPrefab & dropSlot.DropChance == 0) continue;

                    int random = Random.Range(1, 100);
                    if (random < dropSlot.DropChance)
                    {
                        Vector3 spawnPoint = transform.position + (Vector3)Random.insideUnitCircle * 0.125f;
                        var item = Instantiate(dropSlot.DropItemPrefab, spawnPoint, Quaternion.identity);
                        item.transform.parent = folder;
                    }
                }
            }
        }

    }

    public UnitParameter ReturnParameter(ParametersList parameterType)
    {
        foreach (var parameter in Parameters)
            if (parameter.Parameter == parameterType)
                return parameter;
        return null;
    }

    private IEnumerator CorTakeDamage()
    {
        float takeDamage = 1;
        while (takeDamage > 0)
        {
            mobInfo.SpriteRenderer.material.SetFloat("_TakeDamage", takeDamage);
            takeDamage -= Time.deltaTime * 4;
            yield return new WaitForEndOfFrame();
        }
    }
}
