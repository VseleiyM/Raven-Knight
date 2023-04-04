using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atac : MonoBehaviour
{
    public float damage;
    public float SpeedAttac;
    public float TimeAttack = 0;

    private Vector3 pushFrom;
    public float pushPower = 10;
    public bool Atac_Bool = false;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

  

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (Atac_Bool == true)
        {
            HealthPlayer healthP = collision.gameObject.GetComponent<HealthPlayer>();
            if (healthP != null)
            {
                if (TimeAttack < Time.time)
                {
                    healthP.Hit(damage);
                    healthP.PushAway(pushFrom, pushPower);
                    TimeAttack = Time.time + SpeedAttac;
                }

            }
        }
    }
}
