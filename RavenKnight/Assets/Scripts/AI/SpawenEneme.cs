using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawenEneme : MonoBehaviour
{

    public GameObject zombie_prefab; // префаб с мобом
    public GameObject zombie2_prefab; // префаб с мобом
    public GameObject zombie3_prefab; // префаб с мобом
    public GameObject zombie4_prefab;
    public GameObject zombie5_prefabB;
    public List<GameObject> zombie; // список мобов
    public float delayBetweenWaves = 15f; // пауза между волнами
    public int currentWave = 1; // номер волны
    public int LivelWave = 1;// уровень волн
    public Vector3 spawn_point; // точка спавна мобов
    public Vector3 zombie_look; // куда смотреть мобам
    public int WaveCount = 3;

    public int EnemyTypes_1 = 1;
    public int EnemyTypes_2 = 1;
    public int EnemyTypes_3 = 1;
    public int EnemyTypes_4 = 1;

    public float Timere = 0;
    public int Zaderzka = 1;
   

    public float cellX = 1f; // размер ячейки для сетки спавна мобов
    public float cellY = 1f; // размер ячейки для сетки спавна мобов

    public bool waitForZombies = true; // флаг ожидания спавна мобов

    void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        zombie.Clear();
        int zombieCount = currentWave * LivelWave *EnemyTypes_1;; // функция количества мобов от волны
        int zombieCount2 = currentWave * LivelWave* EnemyTypes_2;
        int zombieCount3 = currentWave * LivelWave * EnemyTypes_3;
        int zombieCount4 = currentWave * LivelWave * EnemyTypes_4;
        int zombieCount5B = 1;
        for (int i = 0; i < zombieCount; i++)
        {
            spawn_point = new Vector3(Random.Range(-15f, 15f), Random.Range(-10f, 14f),0);

            GameObject z = (GameObject)Instantiate(zombie_prefab,                  // префаб моба
                                                   GetSpawnPoint(i, zombieCount),  // получаем точку спавна
                                                   GetSpawnDirection(i));          // получаем направление
            zombie.Add(z);
        }
        for (int i = 0; i < zombieCount2; i++)
        {
            spawn_point = new Vector3(Random.Range(-15f, 15f), Random.Range(-10f, 14f), 0);

            GameObject z1 = (GameObject)Instantiate(zombie2_prefab,
                                                   GetSpawnPoint(i, zombieCount2),
                                                   GetSpawnDirection(i));
            zombie.Add(z1);
        }
        for (int i = 0; i < zombieCount3; i++)
        {
            spawn_point = new Vector3(Random.Range(-15f, 15f), Random.Range(-10f, 14f), 0);

            GameObject z2 = (GameObject)Instantiate(zombie3_prefab,
                                                   GetSpawnPoint(i, zombieCount3),
                                                   GetSpawnDirection(i));
            zombie.Add(z2);

        }
        if (currentWave >= 2)
        {
            for (int i = 0; i < zombieCount4; i++)
            {
                spawn_point = new Vector3(Random.Range(-15f, 15f), Random.Range(-10f, 14f), 0);

                GameObject z3 = (GameObject)Instantiate(zombie4_prefab,
                                                       GetSpawnPoint(i, zombieCount4),
                                                       GetSpawnDirection(i));
                zombie.Add(z3);
            }
        }
            
        if(currentWave == 3)
        {
            for (int i = 0; i < zombieCount5B; i++)
            {
                GameObject z = (GameObject)Instantiate(zombie5_prefabB,                  // префаб моба
                                                       GetSpawnPoint(i, zombieCount5B),  // получаем точку спавна
                                                       GetSpawnDirection(i));          // получаем направление
                zombie.Add(z);
            }
        }
        
        WaveCount = WaveCount - 1;
        waitForZombies = false;
    }

    private Vector3 GetSpawnPoint(int num, int max)
    {
        int q1 = (int)Mathf.Sqrt(max); // корень из максимума БЕЗ остатка
        float q2 = Mathf.Sqrt(max);     // корень из максимума С остатком
        q1 = (q1 != q2 ? q1 + 1 : q1);  // если корни не совпадают, то берем (целый+1), иначе просто (целый)
                                        // это у нас размер толпы по горизонтали
        return (spawn_point + // точка спавна плюс сдвиг каждого моба, в зависимости от номера и общего числа
            new Vector3(cellX * (num % q1),   // размер ячейки * (остаток от номера / размер толпы)
            0f,
            cellY * (num / q1))); // размер ячейки * ряд
    }

    private Quaternion GetSpawnDirection(int num)
    {
        return Quaternion.Euler(zombie_look - spawn_point); // каждый смотрит на точку zombie_look
    }

    // Update is called once per frame
    void Update()
    {
        if (waitForZombies) return; // если ожидание спавна - выходим

        if (zombie.Count == 0 || zombie.Count < 0 && WaveCount > 0) // если зомби не осталось
        {
            currentWave++; // счетчик волны +1
            waitForZombies = true; // флаг ожидания спавна
            StartCoroutine(NextWave()); // запуск корутины паузы перед спавном
        }
    }

    IEnumerator NextWave()
    {
        float time = 0; // таймер
        do
        {
            time += Time.deltaTime; // увеличиваем на время кадра
            yield return null; // ждём до следующего кадра
        } while (time < delayBetweenWaves); // повторяем, пока таймер меньше времени спавна
        Spawn(); // ну и спавним, собственно
    }
}
