using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobAction : MonoBehaviour
{
    private MobInfo mobInfo;
    private Transform folder;

    private void Awake()
    {
        var goFolder = GameObject.Find("Temp");
        if (!goFolder)
            folder = new GameObject("Temp").transform;
        else
            folder = goFolder.transform;

        mobInfo = GetComponentInParent<MobInfo>();
    }

    public void SendMobDead()
    {
        GlobalEvents.SendMobDead(mobInfo.Mob);
        GlobalEvents.SendScoreChanged(mobInfo.Mob.GainScore);
        GlobalEvents.SendCreateScoreText(mobInfo.transform.position, mobInfo.Mob.GainScore);
    }

    public void DestroyUnit()
    {
        Destroy(mobInfo.gameObject);
    }

    public void Audio_Attack()
    {
        foreach (var clip in mobInfo.ListAudioClip)
        {
            if (clip.name.ToLower() == "attack")
            {
                mobInfo.AudioSource.clip = clip;
                mobInfo.AudioSource.Play();
                break;
            }
        }
    }

    public void Attack()
    {
        switch ((int)mobInfo.TypeAttack)
        {
            case (int)AIEnumTypeAttack.HitScan:
                Hit();
                break;
            case (int)AIEnumTypeAttack.Projectile:
                CreateProjectile();
                break;
        }

        void Hit()
        {
            if (mobInfo.AttackTrigger.IsTouching(mobInfo.Mob.targetCollider))
                mobInfo.Mob.targetCollider.GetComponent<IDamageable>().TakeDamage(mobInfo.Mob.Damage);
        }

        void CreateProjectile()
        {
            Vector3 spawnPoint = new Vector3(mobInfo.transform.position.x, mobInfo.transform.position.y);
            Vector3 target = mobInfo.Mob.target.position;
            float angle = Mathf.Atan2(target.y - spawnPoint.y, target.x - spawnPoint.x) * Mathf.Rad2Deg;

            var projectile = Instantiate(mobInfo.Projectile, spawnPoint, Quaternion.Euler(0, 0, angle));
            projectile.transform.parent = folder;
            projectile.layer = mobInfo.gameObject.layer;
            Projectile compProjectile = projectile.GetComponent<Projectile>();
            compProjectile.damageableTag = mobInfo.Mob.DamageableTag;
            compProjectile.damage = mobInfo.Mob.Damage;
        }
    }

    public void MobSpawned()
    {
        mobInfo.PhysicsCollider.enabled = true;
        mobInfo.Mob.EnableAI();
    }
}
