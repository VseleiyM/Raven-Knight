using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
    public float damage;
    public int speed;
    /*public float distace;
    public float lifetime;*/
    public LayerMask whatIsSolid;

    private void Update()
    {
        //RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distace, whatIsSolid);
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        Target target = other.gameObject.GetComponent<Target>();


        if (target != null)
        {
            target.Hit(damage);
            Destroy(gameObject); 
        }
    }*/
    private void OnCollisionStay2D(Collision2D other)
    {
        Target target = other.gameObject.GetComponent<Target>();


        if (target != null)
        {
            target.Hit(damage);
            Destroy(gameObject);
        }
    }
}
