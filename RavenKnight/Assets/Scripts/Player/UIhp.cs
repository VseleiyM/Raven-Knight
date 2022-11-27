using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIhp : MonoBehaviour
{

    public Text bar;
    private HealthPlayer h;
    private float HP;


    void Start()
    {
        h = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPlayer>();         
    }

    // Update is called once per frame
    void Update()
    {
        float HP = h.health;
        bar.text = "HP" + HP;
    }
}
