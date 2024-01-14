using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [System.Serializable]
    public class WaveSettings
    {
        public List<int> Settings => _settings;
        [SerializeField] private List<int> _settings;
    }

    public class EnemySpawner : MonoBehaviour
    {
        private BoxCollider2D[] listZones;
        [SerializeField] private List<GameObject> listEnemy;
        [SerializeField] private List<WaveSettings> listWave;
        [SerializeField, Min(0.01f)] private float spawnDelay = 1;
        [SerializeField, Min(0)] private float durationWave = 60;
        [SerializeField] private bool useDurationWave;
        [SerializeField] private bool bossRoom;
        public int ScoreForRoom => _scoreForRoom;
        [SerializeField] private int _scoreForRoom;

        [SerializeField] private bool isTriggered = false;

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

            listZones = GetComponents<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag != "Player" || isTriggered) return;

            GlobalEvents.bossDead += OnMobDead;
            GlobalEvents.mobDead += OnMobDead;

            isTriggered = true;
            currentWave = 0;

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
                    int randZone = Random.Range(0, listZones.Length);
                    Vector3 spawnPoint = transform.position + (Vector3)listZones[randZone].offset;
                    if (!bossRoom)
                        spawnPoint += (Vector3)(listZones[randZone].size * Random.insideUnitCircle) / 2;
                    var enemyGO = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity, folder);
                    var mobInfo = enemyGO.GetComponent<MobInfo>();

                    mobInfo.TargetInfo.Animator.SetFloat("SpawnDelay", 1 / spawnDelay);
                    listLifeEnemy.Add(enemyGO);
                }
            }
        }

        private void OnMobDead(Target target)
        {
            listLifeEnemy.Remove(target.gameObject);
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
                GlobalEvents.bossDead -= OnMobDead;
                GlobalEvents.mobDead -= OnMobDead;

                GlobalEvents.SendOpenRoom(_scoreForRoom);
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
}