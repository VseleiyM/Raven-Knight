using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction1 : MonoBehaviour
{
    [SerializeField] private MobInfo mobInfo;
    [Header("Предсмертное разделение")]
    [SerializeField] private GameObject deathSpawn;
    [Min(0f)]
    [SerializeField] private float spawnRadius = 0f;
    [Min(1)]
    [SerializeField] private int count = 1;

    private Transform units;

    private void Awake()
    {
        var goUnits = GameObject.Find("Units");
        if (!goUnits)
            units = new GameObject("Units").transform;
        else
            units = goUnits.transform;
    }

    public void SendBossDead()
    {
        GlobalEvents.SendBossDead(mobInfo.Mob);
    }

    public void Dead()
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPointV2 = new Vector2(transform.position.x, transform.position.y);
            Vector2 offset = spawnPointV2 + Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPointV3 = new Vector3(offset.x, offset.y, 0f);
            var enemy = Instantiate(deathSpawn, spawnPointV3, Quaternion.identity);
            enemy.transform.parent = units;
        }
        Destroy(mobInfo.gameObject);
    }
}
