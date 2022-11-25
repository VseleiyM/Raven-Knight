using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawenEneme : MonoBehaviour
{
    /*[Range(1, 3)] public int Level;
    public int Waves;
    private bool WavesBool = true;

    [Range(0, 45)] public float maxRoundVariation;
    public Transform transformSootPoint;
    public GameObject Eneme_1;
    public GameObject Eneme_2;
    public GameObject Eneme_3;

    public float SpeedAttac = 1;
    private float TimeSp_1 = 0;
    private float TimeSp_2 = 0;
    private float TimeSp_3 = 0;

    [SerializeField] private int Eneme_1Int;
    [SerializeField] private int Eneme_2Int;
    [SerializeField] private int Eneme_3Int;

    void Start()
    {
        if (Waves > 0)
        {
            if (Level == 1)
            {
                Eneme_1Int = Random.Range(5, 8);
                Eneme_2Int = Random.Range(2, 3);
                Eneme_3Int = Random.Range(1, 2);

            }
            else if (Level == 2)
            {
                Eneme_1Int = Random.Range(8, 12);
                Eneme_2Int = Random.Range(3, 5);
                Eneme_3Int = Random.Range(2, 4);
            }
            else if (Level == 3)
            {
                Eneme_1Int = Random.Range(12, 16);
                Eneme_2Int = Random.Range(5, 8);
                Eneme_3Int = Random.Range(4, 6);
            }
            WavesBool = true;
            StartCoroutine(FlareCountdown());

        }
        else if (Waves < 0)
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {

    }

    void SpaweneOj()
    {
        if (Waves > 0)
        {
            if (Level == 1)
            {
                Eneme_1Int = Random.Range(5, 8);
                Eneme_2Int = Random.Range(2, 3);
                Eneme_3Int = Random.Range(1, 2);

            }
            else if (Level == 2)
            {
                Eneme_1Int = Random.Range(8, 12);
                Eneme_2Int = Random.Range(3, 5);
                Eneme_3Int = Random.Range(2, 4);
            }
            else if (Level == 3)
            {
                Eneme_1Int = Random.Range(12, 16);
                Eneme_2Int = Random.Range(5, 8);
                Eneme_3Int = Random.Range(4, 6);
            }
            Waves -= 1;
            StartCoroutine(FlareCountdown());
        }
        else if (Waves < 0)
        {
            Destroy(gameObject);
        }    
    }

    public float summ = 0;

    public IEnumerator FlareCountdown()
    {
        while (Eneme_1Int > 0)
        {
            Invoke("Level_1", SpeedAttac);
        }
        while (Eneme_2Int > 0)
        {
            Invoke("Level_2", SpeedAttac);
        }
        while (Eneme_3Int > 0)
        {
            Invoke("Level_3", SpeedAttac);
        }
        yield return new WaitForSeconds(10.0f);
        Invoke("SpaweneOj", 1f);
    }





    public void Level_1()
    {
        GameObject spawnedEneme_1 = Instantiate(
                Eneme_1,
                transformSootPoint.position,
                transform.rotation);
        Eneme_1Int -= 1;
    }
    public void Level_2()
    {
        GameObject spawnedEneme_2 = Instantiate(
                Eneme_2,
                transformSootPoint.position,
                transform.rotation);
        Eneme_2Int -= 1;
    }
    public void Level_3()
    {
        GameObject spawnedEneme_3 = Instantiate(
                Eneme_3,
                transformSootPoint.position,
                transform.rotation);
        Eneme_3Int -= 1;
    }*/
    public GameObject zombie_prefab; // префаб с мобом
    public GameObject zombie2_prefab; // префаб с мобом
    public GameObject zombie3_prefab; // префаб с мобом
    public GameObject zombie4_prefab;
    public List<GameObject> zombie; // список мобов
    public float delayBetweenWaves = 15f; // пауза между волнами
    public int currentWave = 1; // номер волны
    public int LivelWave = 1;// уровень волн
    public Vector3 spawn_point; // точка спавна мобов
    public Vector3 zombie_look; // куда смотреть мобам

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
        int zombieCount = currentWave * LivelWave *5; // функция количества мобов от волны
        int zombieCount2 = currentWave * LivelWave*2;
        int zombieCount3 = currentWave * LivelWave;
        int zombieCount4 = currentWave * LivelWave*2;
        for (int i = 0; i < zombieCount; i++)
        {

            GameObject z = (GameObject)Instantiate(zombie_prefab,                  // префаб моба
                                                   GetSpawnPoint(i, zombieCount),  // получаем точку спавна
                                                   GetSpawnDirection(i));          // получаем направление

            zombie.Add(z); // добавляем в список
        }
        for (int i = 0; i < zombieCount2; i++)
        {
            GameObject z1 = (GameObject)Instantiate(zombie2_prefab,
                                                   GetSpawnPoint(i, zombieCount2),
                                                   GetSpawnDirection(i));
            zombie.Add(z1);
        }
        for (int i = 0; i < zombieCount3; i++)
        {
            GameObject z2 = (GameObject)Instantiate(zombie3_prefab,
                                                   GetSpawnPoint(i, zombieCount3),
                                                   GetSpawnDirection(i));
            zombie.Add(z2);
        }
        for (int i = 0; i < zombieCount4; i++)
        {
            GameObject z3 = (GameObject)Instantiate(zombie4_prefab,
                                                   GetSpawnPoint(i, zombieCount4),
                                                   GetSpawnDirection(i));
            zombie.Add(z3);
        }
        delayBetweenWaves *= 2;
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

        if (zombie.Count == 0 || zombie.Count > 0 || zombie.Count < 0) // если зомби не осталось
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
