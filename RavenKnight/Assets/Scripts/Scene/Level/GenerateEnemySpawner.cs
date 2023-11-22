using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GenerateLevel
{
    public class GenerateEnemySpawner : MonoBehaviour
    {
        public BoxCollider2D spawnZone;
        [Space(10)]
        [Min(0)] public int roomIndex = 0;
        public int maxWave;
        public int roomDifficult;
        public List<GameObject> enemyList;
        public float spawnDelay = 3;
        public bool bossRoom = false;
        public bool useDurationWave = false;
        [SerializeField, Min(0)] private float durationWave = 60;

        private int currentWaveID;
        private List<GameObject> listLifeEnemy = new List<GameObject>();
        [HideInInspector] public Room roomInfo;

        private Transform folder;
        private bool isTriggered = false;
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
            if (collision.tag != "Player" || isTriggered) return;

            GlobalEvents.mobSpawned += OnMobSpawned;
            GlobalEvents.bossDead += OnMobDead;
            GlobalEvents.mobDead += OnMobDead;

            isTriggered = true;
            currentWaveID = 0;

            GlobalEvents.SendCloseRoom();
            GlobalEvents.SendNextWave(currentWaveID + 1, bossRoom);

            SpawnWave();

            if (useDurationWave)
                timeToNextWave = StartCoroutine(TimeToNextWave(durationWave));
        }

        private void SpawnWave()
        {
            List<GameObject> spawnList = new List<GameObject>();
            foreach (var enemy in enemyList)
                spawnList.Add(enemy);

            int waveDifficult = GetWaveDifficult(currentWaveID + 1);
            List<Vector2Int> checkingPoints = new List<Vector2Int>();
            Vector3 offset = new Vector3(-roomInfo.size + 1, -roomInfo.size + 1, 0);
            while (true)
            {
                if (spawnList.Count == 0) break;

                Vector3 spawnPoint = transform.position + (Vector3)spawnZone.offset;
                if (!bossRoom)
                {
                    Vector2Int point = new Vector2Int();
                    point.Set(Random.Range(0, roomInfo.size * 2 - 1), Random.Range(0, roomInfo.size * 2 - 1));
                    if (roomInfo.mapObstacle[point.x, point.y])
                    {
                        checkingPoints.Clear();
                        CollectCheckablePoints(point);
                        for (int j = 0; j < checkingPoints.Count; j++)
                        {
                            if (!roomInfo.mapObstacle[checkingPoints[j].x, checkingPoints[j].y])
                            {
                                point.Set(checkingPoints[j].x, checkingPoints[j].y);
                                break;
                            }
                            else
                            {
                                CollectCheckablePoints(checkingPoints[j]);
                            }
                        }

                        void CollectCheckablePoints(Vector2Int point)
                        {
                            Vector2Int newPoint = new Vector2Int(point.x, point.y + 1);
                            if (0 <= newPoint.y && newPoint.y < roomInfo.size * 2 - 1)
                                if (!checkingPoints.Contains(newPoint))
                                    checkingPoints.Add(newPoint);

                            newPoint = new Vector2Int(point.x + 1, point.y);
                            if (0 <= newPoint.x && newPoint.x < roomInfo.size * 2 - 1)
                                if (!checkingPoints.Contains(newPoint))
                                    checkingPoints.Add(newPoint);

                            newPoint = new Vector2Int(point.x, point.y - 1);
                            if (0 <= newPoint.y && newPoint.y < roomInfo.size * 2 - 1)
                                if (!checkingPoints.Contains(newPoint))
                                    checkingPoints.Add(newPoint);

                            newPoint = new Vector2Int(point.x - 1, point.y);
                            if (0 <= newPoint.x && newPoint.x < roomInfo.size * 2 - 1)
                                if (!checkingPoints.Contains(newPoint))
                                    checkingPoints.Add(newPoint);
                        }
                    }

                    spawnPoint += new Vector3(point.x, point.y, 0) + offset;
                }

                int mobID = Random.Range(0, spawnList.Count);
                var mobGO = Instantiate(spawnList[mobID], spawnPoint, Quaternion.identity, folder);
                var mobInfo = mobGO.GetComponent<MobInfo>();

                int mobCost = (int)mobInfo.TargetInfo.Target.ReturnParameter(ParametersList.GainScore).Max;
                int remainsDifficult = waveDifficult - mobCost;

                if (bossRoom)
                {
                    spawnList.Remove(spawnList[mobID]);
                    mobInfo.TargetInfo.Animator.SetFloat("SpawnDelay", 1 / spawnDelay);
                }
                else if (remainsDifficult < 0)
                {
                    spawnList.Remove(spawnList[mobID]);
                    listLifeEnemy.Remove(mobGO);
                    Destroy(mobGO);
                }
                else
                {
                    waveDifficult = remainsDifficult;
                    mobInfo.TargetInfo.Animator.SetFloat("SpawnDelay", 1 / spawnDelay);
                }
            }
        }

        private int GetWaveDifficult(int wave)
        {
            int divider = 0;
            for (int i = 1; i <= maxWave; i++)
                divider += i;

            int result = (roomDifficult / divider) * wave;

            return result;
        }

        private void NextWave()
        {
            currentWaveID++;
            if (currentWaveID < maxWave)
            {
                GlobalEvents.SendNextWave(currentWaveID + 1, bossRoom);
                SpawnWave();
            }
            else
            {
                GlobalEvents.mobSpawned -= OnMobSpawned;
                GlobalEvents.bossDead -= OnMobDead;
                GlobalEvents.mobDead -= OnMobDead;

                GlobalEvents.SendOpenRoom(0);
                if (bossRoom)
                    GlobalEvents.SendBossRoomClear();
            }

            if (useDurationWave)
                timeToNextWave = StartCoroutine(TimeToNextWave(durationWave));
        }

        private void OnMobSpawned(Target target)
        {
            listLifeEnemy.Add(target.gameObject);
        }

        private void OnMobDead(Target target)
        {
            listLifeEnemy.Remove(target.gameObject);
            if (listLifeEnemy.Count == 0)
                NextWave();
        }

        private IEnumerator TimeToNextWave(float duration)
        {
            while (duration > 0)
            {
                duration -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            if (currentWaveID < maxWave - 1)
                NextWave();
        }
    }
}