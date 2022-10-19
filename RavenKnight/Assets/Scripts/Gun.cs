using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //public float offset;

    public bool lookAtCursor;
    public enum ShootState
    {
        Ready,
        Shooting,
        Reloading
    }

    // Как далеко вперед дуло находится от центра пистолета
    public Transform transformSootPoint;

    [Header("Magazine")]
    public GameObject round;
    public int ammunition;

    [Range(0.5f, 10)] public float reloadTime;

    private int remainingAmmunition;

    [Header("Shooting")]
    // Сколько выстрелов пистолет может сделать в секунду
    [Range(0.25f, 25)] public float fireRate;

    // Количество выстрелов в каждом выстреле
    public int roundsPerShot;

    private ShootState shootState = ShootState.Ready;

    // В следующий раз, когда пистолет сможет стрелять в
    private float nextShootTime = 0;

    void Start()
    {
        remainingAmmunition = ammunition;
    }

    void Update()
    {
        switch (shootState)
        {
            case ShootState.Shooting:
                // Если пистолет снова готов к стрельбе...
                if (Time.time > nextShootTime)
                {
                    shootState = ShootState.Ready;
                }
                break;
            case ShootState.Reloading:
                // Если пистолет закончил перезаряжаться...
                if (Time.time > nextShootTime)
                {
                    remainingAmmunition = ammunition;
                    shootState = ShootState.Ready;
                }
                break;
        }
        if (lookAtCursor)
        {
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            lookPos = lookPos - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            /*Vector3 lookPos = Camera.main.ScreenToWorldPoint((Input.mousePosition) - transform.position);
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);*/

        }
    }

    /// Попытки выстрелить из пистолета
    public void Shoot()
    {
        // Проверяет, что пистолет готов к стрельбе
        if (shootState == ShootState.Ready)
        {
            for (int i = 0; i < roundsPerShot; i++)
            {
                // Создает экземпляр снаряда в положении дула
                GameObject spawnedRound = Instantiate(
                    round,
                    transformSootPoint.position,
                    transform.rotation
                );
            }

            remainingAmmunition--;
            if (remainingAmmunition > 0)
            {
                nextShootTime = Time.time + (1 / fireRate);
                shootState = ShootState.Shooting;
            }
            else
            {
                Reload();
            }
        }
    }

    /// Попытки перезарядить пистолет
    public void Reload()
    {
        // Проверяет, что пистолет готов к перезарядке
        if (shootState == ShootState.Ready)
        {
            nextShootTime = Time.time + reloadTime;
            shootState = ShootState.Reloading;
        }
    }
}
