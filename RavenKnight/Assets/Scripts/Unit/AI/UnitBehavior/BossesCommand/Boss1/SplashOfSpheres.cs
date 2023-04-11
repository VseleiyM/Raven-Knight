using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashOfSpheres : UnitCommand
{
    public override UnitCommand NextStep => _nextStep;
    [SerializeField] private UnitCommand _nextStep;
    [Space(10)]
    [SerializeField] private GameObject projectilePrefab;
    [Min(1)]
    [SerializeField] private int count = 1;

    private Transform temp;

    private void Awake()
    {
        var goTemp = GameObject.Find("Temp");
        if (!goTemp)
            temp = new GameObject("Temp").transform;
        else
            temp = goTemp.transform;
    }

    public override void RequestData(MobInfo mobInfo)
    {

    }

    public override void Execute()
    {
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y, 0f);
        float angleStep = 360f / count;
        for (int i = 0; i < count; i++)
        {
            var projectile = Instantiate(projectilePrefab, spawnPoint, Quaternion.Euler(0f, 0f, angleStep * i));
            projectile.transform.parent = temp;
            var compProjectile = projectile.GetComponent<Projectile>();
            compProjectile.gameObject.layer = gameObject.layer;
            compProjectile.damageableTag = DamageableTag.All;
        }
    }
}
