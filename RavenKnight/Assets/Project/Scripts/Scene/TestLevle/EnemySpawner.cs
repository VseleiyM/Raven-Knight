using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveSettings
{
    public List<int> Settings => _settings;
    [SerializeField] private List<int> _settings;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private BoxCollider2D trigger;
    [SerializeField] private List<GameObject> listEnemy;
    [SerializeField] private List<WaveSettings> listWave;
    [SerializeField] private float spawnDelay;


    [SerializeField] private bool isClear = false;
    [SerializeField] private bool triggered = false;

    private int currentWave;
    private List<GameObject> listLifeEnemy = new List<GameObject>();

    private Transform folder;

    private void Awake()
    {
        var goTemp = GameObject.Find("Units");
        if (!goTemp)
            folder = new GameObject("Units").transform;
        else
            folder = goTemp.transform;
    }

    private void OnEnable()
    {
        GlobalEvents.mobDead += OnMobDead;
    }

    private void OnDisable()
    {
        GlobalEvents.mobDead -= OnMobDead;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" || triggered) return;

        triggered = true;
        GlobalEvents.SendCloseRoom();
        SpawnWave();
    }

    private void SpawnWave()
    {
        WaveSettings currentSettings = listWave[currentWave];

        int lenght;
        if (currentSettings.Settings.Count > listEnemy.Count)
            lenght = listEnemy.Count;
        else
            lenght = currentSettings.Settings.Count;

        for (int i = 0; i < lenght; i++)
        {
            GameObject enemyPrefab = listEnemy[i];
            if (!enemyPrefab) continue;

            for (int i1 = 0; i1 < currentSettings.Settings[i]; i1++)
            {
                Vector3 spawnPoint = transform.position + (Vector3)trigger.offset;
                spawnPoint += (Vector3)(trigger.size * Random.insideUnitCircle) / 2;
                var enemyGO = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                enemyGO.transform.parent = folder;
                var mobInfo = enemyGO.GetComponent<MobInfo>();
                mobInfo.Animator.SetFloat("SpawnDelay", 1 / spawnDelay);
                listLifeEnemy.Add(enemyGO);
            }
        }
    }

    private void OnMobDead(Mob mob)
    {
        listLifeEnemy.Remove(mob.gameObject);
        if (listLifeEnemy.Count == 0)
        {
            currentWave++;
            if (currentWave < listWave.Count)
            {
                SpawnWave();
            }
            else
            {
                isClear = true;
                GlobalEvents.SendOpenRoom();
            }
        }
    }
}
