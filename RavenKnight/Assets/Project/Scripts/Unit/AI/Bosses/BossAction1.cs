using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction1 : MonoBehaviour
{
    [Header("Предсмертное разделение")]
    [SerializeField] private GameObject deathSpawn;
    [SerializeField, Min(0f)] private float spawnRadius = 0f;
    [SerializeField, Min(1)] private int count = 1;

    private Transform units;
    private MobInfo mobInfo;

    private void Awake()
    {
        var goUnits = GameObject.Find("Units");
        if (!goUnits)
            units = new GameObject("Units").transform;
        else
            units = goUnits.transform;
        mobInfo = GetComponentInParent<MobInfo>();
    }

    public void SendBossDead()
    {
        GlobalEvents.SendBossDead(mobInfo.TargetInfo.Target);
        UnitParameter scoreParam = mobInfo.TargetInfo.Target.ReturnParameter(ParametersList.GainScore);
        if (scoreParam != null)
        {
            GlobalEvents.SendScoreChanged((int)scoreParam.Max);
            GlobalEvents.SendCreateScoreText(mobInfo.transform.position, (int)scoreParam.Max);
        }
        Destroy(mobInfo.gameObject);
    }

    public void BossDead()
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPointV2 = new Vector2(transform.position.x, transform.position.y);
            Vector2 offset = spawnPointV2 + Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPointV3 = new Vector3(offset.x, offset.y, 0f);
            var enemy = Instantiate(deathSpawn, spawnPointV3, Quaternion.identity);
            enemy.transform.parent = units;
            var mobInfo = enemy.GetComponent<MobInfo>();
            mobInfo.TargetInfo.Animator.SetFloat("SpawnDelay", 6);
        }
    }
}
