using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreateProjectileConus : UnitCommand
{
    [SerializeField] private UnitCommand _nextStep;
    [Space(10)]
    [SerializeField] private GameObject prefabProjectile;
    [SerializeField, Min(1)] private int projectileCount = 1;
    [SerializeField] private float corner;
    [SerializeField] private DamageableTag damageableTag;
    [SerializeField] private float damage;
    [SerializeField] private float projectileSpeed;

    private MobInfo mobInfo;
    private Transform folder;

    private void Awake()
    {
        var goFolder = GameObject.Find("Temp");
        if (!goFolder)
            folder = new GameObject("Temp").transform;
        else
            folder = goFolder.transform;
    }

    public override UnitCommand NextStep => _nextStep;

    public override void RequestData(MobInfo mobInfo)
    {
        this.mobInfo = mobInfo;
    }

    public override void Execute()
    {
        Vector3 spawnPoint = mobInfo.PointForProjectile.transform.position;
        Vector3 target = mobInfo.Mob.target.position;
        float angle = Mathf.Atan2(target.y - spawnPoint.y, target.x - spawnPoint.x) * Mathf.Rad2Deg;
        if (projectileCount != 1)
            angle += corner / 2;

        for (int i = 0; i < projectileCount; i++)
        {
            var projectile = Instantiate(mobInfo.Projectile, spawnPoint, Quaternion.Euler(0, 0, angle));
            projectile.transform.parent = folder;
            if (gameObject.layer == (int)LayerName.Enemy)
                projectile.layer = (int)LayerName.EnemyProjectile;
            Projectile compProjectile = projectile.GetComponent<Projectile>();
            compProjectile.damageableTag = damageableTag;
            compProjectile.damage = damage;
            compProjectile.speed = projectileSpeed;
            angle -= corner / (projectileCount - 1);
        }
    }

}
