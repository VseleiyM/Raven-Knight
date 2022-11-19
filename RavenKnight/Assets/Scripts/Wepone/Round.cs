using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
    
    public int speed;
    public float distace;
    public float lifetime;
    public int penetration;

    public float damage;
    //public RaycastHit2D hitInfo;

    private Vector3 pushFrom;
    public float pushPower = 10;

    private void Start()
    {
    }

    private void Update()
    {
        //RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distace, whatIsSolid);
        transform.Translate(Vector2.right * speed * Time.deltaTime);       

        if (penetration <= 0)
            {
                Destroy(gameObject);
            }

        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Target target = other.gameObject.GetComponent<Target>();

        pushFrom = new Vector3(transform.position.x, transform.position.y);

        if (target != null)
        {            
            if (target.Nohealth == true)
            {
                target.Hit(damage);
                target.PushAway(pushFrom, pushPower);
                penetration -= 1;
            }
            else if (target.Nohealth == false) penetration -= penetration;

        }
    }
}
