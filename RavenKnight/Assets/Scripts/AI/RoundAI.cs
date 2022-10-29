using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundAI : MonoBehaviour
{
    public int speed;
    public float damage;

    private Vector3 pushFrom;
    void Start()
    {
    }
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HealthPlayer healthP = other.gameObject.GetComponent<HealthPlayer>();
        if (healthP != null)
        {
            healthP.Hit(damage);
            Destroy(gameObject);
        }
    }
}
