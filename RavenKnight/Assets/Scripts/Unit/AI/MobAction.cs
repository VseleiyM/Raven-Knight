using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAction : MonoBehaviour
{
    [SerializeField] private MobInfo mobInfo;
    [SerializeField] private string damageableTag;

    private Transform temp;

    private void Awake()
    {
        var goTemp = GameObject.Find("Temp");
        if (!goTemp)
            temp = new GameObject("Temp").transform;
        else
            temp = goTemp.transform;
    }

    public void Attack()
    {
        switch ((int)mobInfo.TypeAttack)
        {
            case (int)TypeAttack.HitScan:
                Hit();
                break;
            case (int)TypeAttack.Projectile:
                CreateProjectile();
                break;
        }

        void Hit()
        {
            List<Collider2D> results = new List<Collider2D>();
            mobInfo.AttackTrigger.GetContacts(results);
            foreach (var collider in results)
                if (collider.tag == damageableTag)
                {
                    collider.GetComponent<IDamageable>().TakeDamage(0f);
                    break;
                }
        }

        void CreateProjectile()
        {
            Vector3 spawnPoint = new Vector3(mobInfo.transform.position.x, mobInfo.transform.position.y);
            Vector3 target = mobInfo.target.position;
            float angle = Mathf.Atan2(target.y - spawnPoint.y, target.x - spawnPoint.x) * Mathf.Rad2Deg;
            var projectile = Instantiate(mobInfo.Projectile, spawnPoint, Quaternion.Euler(0, 0, angle));
            projectile.transform.parent = temp;
            projectile.layer = mobInfo.gameObject.layer;
            Projectile compProjectile = projectile.GetComponent<Projectile>();
            compProjectile.damageableTag = damageableTag;
            compProjectile.coroutine = compProjectile.StartCoroutine(compProjectile.MoveRight());
        }
    }
}
