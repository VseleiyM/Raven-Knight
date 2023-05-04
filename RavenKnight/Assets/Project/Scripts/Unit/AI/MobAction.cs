using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void SendMobDead()
    {
        var goScore = Instantiate(TestLevle.instance.Prefabs.TextScore, TestLevle.instance.CanvasWorldPosition);
        goScore.transform.position = mobInfo.transform.position;
        Text textScore = goScore.GetComponent<Text>();
        textScore.text = $"+{mobInfo.Mob.GainScore}";
        GlobalEvents.SendMobDead(mobInfo.Mob);
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
            case (int)TypeAttack.HitScan:
                Hit();
                break;
            case (int)TypeAttack.Projectile:
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
            projectile.transform.parent = temp;
            projectile.layer = mobInfo.gameObject.layer;
            Projectile compProjectile = projectile.GetComponent<Projectile>();
            compProjectile.damageableTag = mobInfo.Mob.DamageableTag;
            compProjectile.damage = mobInfo.Mob.Damage;
        }
    }
}
