using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealElement : MonoBehaviour
{
    public Collider2D Trigger { get => _trigger; }
    [SerializeField] private Collider2D _trigger;
    public DamageableTag damageableTag;
    public float damage;
    [Min(0.01f)] public float tickInSecond = 1;
    [Min(0.01f)] public float duration = 1;
    private bool hasHit = false;
    [SerializeField] private GameObject collidedObject; 

    private void Start()
    {
        StartCoroutine(ApplyDamageOverTime());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasHit)
        {
            if (damageableTag == DamageableTag.All || collision.CompareTag(damageableTag.ToString()))
            {
                collidedObject = collision.gameObject; // Store the collided game object
                StartCoroutine(ApplyDamageOverTime());
                hasHit = true;
            }
        }
    }

    private IEnumerator ApplyDamageOverTime()
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            yield return new WaitForSeconds(tickInSecond);
            if (collidedObject != null)
            {
                if (damageableTag == DamageableTag.All || collidedObject.CompareTag(damageableTag.ToString()))
                {
                    collidedObject.GetComponent<Target>().TakeDamage(damage);
                }
                timeElapsed += tickInSecond;
            }
            else
            {
                break;
            }
        }

        Destroy(gameObject);
    }
}
