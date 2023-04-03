using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashOfSpheres : UnitCommand
{
    public override UnitCommand NextStep => _nextStep;
    [SerializeField] private UnitCommand _nextStep;

    [Min(0)]
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [Min(1)]
    [SerializeField] private int count = 1;
    [SerializeField] private string damageableTag;

    private bool isReady = true;
    private Vector3 target;
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
        target = mobInfo.target.position;
    }

    public override void Execute()
    {
        if (!isReady) return;

        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y, 0f);
        float angleStep = 360f / count;
        for (int i = 0; i < count; i++)
        {
            var projectile = Instantiate(projectilePrefab, spawnPoint, Quaternion.Euler(0f, 0f, angleStep * i));
            projectile.transform.parent = temp;
            var compProjectile = projectile.GetComponent<Projectile>();
            compProjectile.gameObject.layer = gameObject.layer;
            compProjectile.damageableTag = damageableTag;
            compProjectile.coroutine = compProjectile.StartCoroutine(compProjectile.MoveRight());
        }

        isReady = false;
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        isReady = true;
    }
}
