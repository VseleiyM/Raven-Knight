using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TargetInfo))]
public class Target : MonoBehaviour
{
    [SerializeField] private List<UnitParameter> _parameters;
    [SerializeField] private List<DropSlot> dropList;
    [SerializeField, Min(0)] private float invincibleDuration = 0;

    private TargetInfo targetInfo;
    private Transform folder;

    private bool isDead = false;
    private bool invincible;
    private bool _offTakeDamage;
    private Coroutine corTakeDamage;
    private Coroutine corInvincible;

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
    }

    private void Start()
    {
        targetInfo = GetComponent<TargetInfo>();
    }

    private void OnValidate()
    {
        foreach (var parameter in _parameters)
        {
            parameter.name = parameter.Parameter.ToString();
        }
    }

    public void TakeDamage(float damage)
    {
        if (_offTakeDamage) return;
        if (invincible) return;

        UnitParameter healthParameter = ReturnParameter(ParametersList.Health);

        if (healthParameter == null) return;

        healthParameter.current -= damage;
        GlobalEvents.SendTargetTakeDamage(this, tag);
        if (healthParameter.current > 0)
        {
            if (damage > 0)
            {
                TakeDamageEffect();
                if (targetInfo.MobInfo != null)
                {
                    targetInfo.MobInfo.MobAction.SendTakeDamage();
                    targetInfo.MobInfo.TakeDamage();
                }
                ActiveInvincibleEffect(invincibleDuration);
            }
        }
        else
        {
            if (isDead) return;
            isDead = true;
            StopAllCoroutines();
            targetInfo.PhysicsCollider.enabled = false;
            DropItemLogic();
            targetInfo.Animator.SetTrigger(AnimatorParameter.Dead.ToString());
        }

        void DropItemLogic()
        {
            foreach (var dropSlot in dropList)
            {
                int min = dropSlot.Min;
                int max = dropSlot.Max;
                if (min > max) { max = min; }
                if (max == 0) continue;
                max++;

                if (!dropSlot.OneItem)
                {
                    foreach (var itemPrefab in dropSlot.DropItemPrefab)
                    {
                        int random = Random.Range(min, max);
                        for (int i = 0; i < random; i++)
                        {
                            Vector3 spawnPoint = transform.position + (Vector3)Random.insideUnitCircle * 0.125f;
                            Instantiate(itemPrefab, spawnPoint, Quaternion.identity, folder);
                        }
                    }
                }
                else
                {
                    int random = Random.Range(min, max);
                    int randomItem = Random.Range(0, dropSlot.DropItemPrefab.Count);
                    for (int i = 0; i < random; i++)
                    {
                        Vector3 spawnPoint = transform.position + (Vector3)Random.insideUnitCircle * 0.125f;
                        Instantiate(dropSlot.DropItemPrefab[randomItem], spawnPoint, Quaternion.identity, folder);
                    }
                }
            }
        }

        void TakeDamageEffect()
        {
            if (corTakeDamage != null)
                StopCoroutine(corTakeDamage);

            corTakeDamage = StartCoroutine(CorTakeDamage());
        }
    }

    public void ChangeInvincible()
    {
        _offTakeDamage = !_offTakeDamage;
    }

    public void ActiveInvincibleEffect(float duration)
    {
        if (corInvincible != null)
            StopCoroutine(corInvincible);

        corInvincible = StartCoroutine(InvincibleOn(duration));
    }

    public UnitParameter ReturnParameter(ParametersList parameterType)
    {
        foreach (var parameter in _parameters)
            if (parameter.Parameter == parameterType)
                return parameter;
        return null;
    }

    private IEnumerator CorTakeDamage()
    {
        float takeDamage = 1;
        while (takeDamage > 0)
        {
            targetInfo.SpriteRenderer.material.SetFloat("_TakeDamage", takeDamage);
            takeDamage -= Time.deltaTime * 4;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator InvincibleOn(float duration)
    {
        invincible = true;
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        invincible = false;
    }
}
