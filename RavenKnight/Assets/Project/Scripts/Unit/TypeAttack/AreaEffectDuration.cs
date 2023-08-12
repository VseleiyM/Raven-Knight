using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectDuration : MonoBehaviour
{
    public Collider2D Trigger { get => _trigger; }
    [SerializeField] private Collider2D _trigger;
    [SerializeField] private Animator animator;
    [Space(10)]
    public DamageableTag damageableTag;
    public float damage;
    [Min(1)] public float tickInSecond = 1;
    [Min(0.01f)] public float duration = 1;

    private void Start()
    {
        RefreshData();
        StartCoroutine(LifeTime());
    }

    public void RefreshData()
    {
        animator.SetFloat("TickDuration", tickInSecond);
    }

    public void HitArea()
    {
        List<Collider2D> results = new List<Collider2D>();
        _trigger.GetContacts(results);
        foreach (var collision in results)
        {
            if (damageableTag == DamageableTag.All
                && collision.CompareTag(damageableTag.ToString()))
                collision.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }

    private IEnumerator LifeTime()
    {
        float lifeTime = 0;

        while (lifeTime < duration)
        {
            lifeTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }
}
