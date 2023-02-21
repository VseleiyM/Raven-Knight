﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{

    public float health = 100;
    public float maxhealth = 100;
    public float Defense = 5;

    private Rigidbody2D _rBody;
    private Vector3 TargetTransform;

    public GameObject spawneKill;
    private Vector3 Posiceon;

    public float pushPowerObj = 1;



    public UnityEngine.Object explosion;
    public Material matBlink;

    private Material matDefault;
    private SpriteRenderer spriteRend;

    private void Start()
    {

        _rBody = GetComponent<Rigidbody2D>();

        spriteRend = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("EnemyBlink", typeof(Material)) as Material;
        matDefault = spriteRend.material;

        explosion = Resources.Load("Explosion");
    }

    private void Update()
    {
        Posiceon = gameObject.transform.position;
    }


    //Урон получение
    public void Hit(float damage)
    {
        if(Defense <= 0)
        {
            health -= damage;
        }
        else
        {
            Defense -= damage;
        }
        
        if (health <= 0)
        {
            Invoke("KillEnemy", 0f);
        }
        else if (health > maxhealth)
        {
            health = maxhealth;
        }
        else
        {
            Invoke("ResetMaterial", 0.1f);
        }
        spriteRend.material = matBlink;
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
        spriteRend.material = matDefault;
    }
    void KillEnemy()
    {
        GameObject explosionRef = (GameObject)Instantiate(explosion);
        explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        GameObject spawnedRound = (GameObject)Instantiate(spawneKill, Posiceon, transform.rotation);
        Destroy(gameObject);
    }
}
