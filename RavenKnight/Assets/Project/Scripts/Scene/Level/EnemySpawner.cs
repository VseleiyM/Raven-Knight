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
    [SerializeField, Min(0.01f)] private float spawnDelay = 1;
    [SerializeField, Min(0)] private float durationWave = 60;
    [SerializeField] private bool useDurationWave;
    [SerializeField] private bool bossRoom;
    public int ScoreForRoom => _scoreForRoom;
    [SerializeField] private int _scoreForRoom;

    [SerializeField] private bool isClear = false;
    [SerializeField] private bool triggered = false;

    private int currentWave;
    private List<GameObject> listLifeEnemy = new List<GameObject>();

    private Transform folder;
    private Coroutine timeToNextWave;

    private void Awake()
    {
        var goFolder = GameObject.Find("Units");
        if (!goFolder)
            folder = new GameObject("Units").transform;
        else
            folder = goFolder.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" || triggered) return;

        GlobalEvents.mobSpawned += OnMobSpawned;
        GlobalEvents.bossDead += OnMobDead;
        GlobalEvents.mobDead += OnMobDead;

        triggered = true;
        GlobalEvents.SendCloseRoom();
        GlobalEvents.SendNextWave(currentWave + 1, bossRoom);
        SpawnWave();
        if (useDurationWave)
            timeToNextWave = StartCoroutine(TimeToNextWave(durationWave));
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
                if (!bossRoom)
                    spawnPoint += (Vector3)(trigger.size * Random.insideUnitCircle) / 2;
                var enemyGO = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                enemyGO.transform.parent = folder;
                var mobInfo = enemyGO.GetComponent<MobInfo>();

                mobInfo.Animator.SetFloat("SpawnDelay", 1 / spawnDelay);
            }
        }
    }

    private void OnMobSpawned(Mob mob)
    {
        listLifeEnemy.Add(mob.gameObject);
    }

    private void OnMobDead(Mob mob)
    {
        listLifeEnemy.Remove(mob.gameObject);
        if (listLifeEnemy.Count == 0)
        {
            NextWave();
        }
    }

    private void NextWave()
    {
        currentWave++;
        if (currentWave < listWave.Count)
        {
            GlobalEvents.SendNextWave(currentWave + 1, bossRoom);
            SpawnWave();
        }
        else
        {
            GlobalEvents.mobSpawned -= OnMobSpawned;
            GlobalEvents.bossDead -= OnMobDead;
            GlobalEvents.mobDead -= OnMobDead;

            isClear = true;
            GlobalEvents.SendOpenRoom(this);
            if (bossRoom)
                GlobalEvents.SendBossRoomClear();
        }

        if (useDurationWave)
            timeToNextWave = StartCoroutine(TimeToNextWave(durationWave));
    }

    private IEnumerator TimeToNextWave(float duration)
    {
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (currentWave < listWave.Count - 1)
            NextWave();
    }
}
