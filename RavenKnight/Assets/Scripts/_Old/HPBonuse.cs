using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBonuse : MonoBehaviour
{

    public float HP = 5;

    public void OnTriggerStay2D(Collider2D collision)
    {

        HealthPlayer healthP = collision.gameObject.GetComponent<HealthPlayer>();
        if (healthP.health < healthP.maxhealth)
        {
            healthP.Hit((-HP));
            Destroy(gameObject);
        }
    }
}
