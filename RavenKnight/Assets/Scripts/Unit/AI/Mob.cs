using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour, IDamageable
{
    public float CurrentHealth { get => _currentHealth; }
    [SerializeField] private float _currentHealth;
    public float MaxHealth { get => _maxHealth; }
    [SerializeField] private float _maxHealth;
    public float Damage { get => _damage; }
    [SerializeField] private float _damage;
    public DamageableTag DamageableTag { get => _damageableTag; }
    [SerializeField] private DamageableTag _damageableTag;
    [SerializeField] private UnitCommand startStep;

    [Space(10)]
    public Transform target;
    public Collider2D targetCollider;
    public Coroutine corotine_AI;

    private MobInfo mobInfo;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        mobInfo = GetComponent<MobInfo>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetCollider = target.GetComponent<Collider2D>();

        corotine_AI = StartCoroutine(EnableAI(startStep));
    }

    private IEnumerator EnableAI(UnitCommand firstStep)
    {
        firstStep.RequestData(mobInfo);
        firstStep.Execute();
        UnitCommand step = firstStep.NextStep;
        yield return new WaitForFixedUpdate();

        while (true)
        {
            step.RequestData(mobInfo);
            step.Execute();
            step = step.NextStep;
            yield return new WaitForFixedUpdate();
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = _currentHealth - damage;

        if (CurrentHealth <= 0)
        {
            StopAllCoroutines();
            mobInfo.Agent.isStopped = true;
            mobInfo.Animator.SetTrigger("Dead");
        }
    }

    public bool ReturnParameter(int id, ref float result)
    {
        bool found = false;

        switch (id)
        {
            case (int)ParametersList.Health:
                result = CurrentHealth;
                found = true;
                break;
        }

        return found;
    }
}
