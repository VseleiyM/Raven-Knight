using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // жизниные силы
    public float health;
    public bool Nohealth = true;

    // отталкиваемость
    private Rigidbody2D _rBody;
    private Vector3 TargetTransform;
    public bool Mig = true;
    public float pushPowerObj = 1;

    //контроль списка волоны ,стоет вынести в отдельный скрипт
    public SpawenEneme Spawn;

    //выподение обектов при смерти
    public bool objsave;
    public SpawenBonus Bonus;

    //для мигания
    public UnityEngine.Object explosion;
    public Material matBlink;

    private Material matDefault;
    private SpriteRenderer spriteRend;

    private void Start()
    {
        _rBody = GetComponent<Rigidbody2D>();
        Spawn = GameObject.FindGameObjectWithTag("Spawen").GetComponent<SpawenEneme>();

        Bonus = GetComponent<SpawenBonus>();


        if (Nohealth == true)
        {
            spriteRend = GetComponent<SpriteRenderer>();
            matBlink = Resources.Load("EnemyBlink", typeof(Material)) as Material;
            matDefault = spriteRend.material;
        }
        

        explosion = Resources.Load("Explosion");
    }

    private void Update()
    {
        
    }

    public void RemoveAtItme(SpawenEneme zombie)
    {
        //zombie.RemoveAt(zombie_gameobject);
    }


    //Урон получение
    public void Hit(float damage)
    {
        
        health -= damage;
        if (health <= 0)
        {

            Invoke("KillEnemy", 0f);
        }
        else
        {
            Invoke("ResetMaterial", 0.1f);
        }
        if (Mig == true)
        {
            spriteRend.material = matBlink;
        }
        else
        {
            GameObject explosionRef = (GameObject)Instantiate(explosion);
            explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

    }


    //Отскок
    public void PushAway(Vector3 pushFrom, float pushPower)
    {
        if (_rBody == null || pushPower == 0)
        {
            return;
        }


        var pushDirection = ((pushFrom - transform.position) * -1);

        _rBody.AddForce(pushDirection * (pushPower * pushPowerObj));
    }
    
    /*public void PenetrSpeed(RaycastHit2D hitInfo)
    {

    }*/


    void ResetMaterial()
    {
        if(Mig == true)
        {
            spriteRend.material = matDefault;
        }
        
    }
    void KillEnemy()
    {
        GameObject explosionRef = (GameObject)Instantiate(explosion);
        explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        // вынести 
        if (Spawn != null)
        {
            Spawn.zombie.Remove(gameObject);
        }
        if (objsave == true)
        {
            Bonus.SPOJ();
        }
        Destroy(gameObject);
    }
}
