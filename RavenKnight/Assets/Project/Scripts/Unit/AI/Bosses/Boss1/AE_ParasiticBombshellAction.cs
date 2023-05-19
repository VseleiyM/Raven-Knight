using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AE_ParasiticBombshellAction : MonoBehaviour
{
    [SerializeField] private AreaEffectSingle areaEffect;
    [SerializeField] private GameObject enemyPrefab;

    private Transform units;

    private void Awake()
    {
        var goUnits = GameObject.Find("Units");
        if (!goUnits)
            units = new GameObject("Units").transform;
        else
            units = goUnits.transform;
    }

    public void PBHitArea()
    {
        areaEffect.HitArea();
        var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.transform.parent = units;
        var mobInfo = enemy.GetComponent<MobInfo>();
        mobInfo.Animator.SetFloat("SpawnDelay", 6);
    }
}
