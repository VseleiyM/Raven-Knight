using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAction : MonoBehaviour
{
    private MobInfo mobInfo;
    private Transform temp;

    private void Awake()
    {
        var goTemp = GameObject.Find("Temp");
        if (!goTemp)
            temp = new GameObject("Temp").transform;
        else
            temp = goTemp.transform;

        mobInfo = GetComponentInParent<MobInfo>();
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
            if (mobInfo.AttackTrigger.IsTouching(mobInfo.targetCollider))
                mobInfo.targetCollider.GetComponent<IDamageable>().TakeDamage(0f);
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
            compProjectile.damageableTag = mobInfo.Mob.DamageableTag;
        }
    }
}
