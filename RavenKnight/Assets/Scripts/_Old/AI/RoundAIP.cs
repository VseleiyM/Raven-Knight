using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundAIP : MonoBehaviour
{
    public int speed;
    public float damage;

    public Transform playerTr;
    public GameObject Luga;
    private float ZapesX;
    private float ZapesY;


    private Vector3 pushFrom;
    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ZapesX = 0 + playerTr.transform.position.x;
        ZapesY = 0 + playerTr.transform.position.y;
    }
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (gameObject.transform.position.x >= ZapesX && gameObject.transform.position.y >= ZapesY)
        {
            Instantiate(Luga, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HealthPlayer healthP = other.gameObject.GetComponent<HealthPlayer>();
        Target target = other.gameObject.GetComponent<Target>();
        if (healthP != null)
        {
            healthP.Hit(damage);
            Destroy(gameObject);
        }
        else if (target != null)
        {
            if (target.Nohealth == false)
            {
                Destroy(gameObject);
            }

        }
    }
}
