using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEvents : MonoBehaviour
{
    public UnityAction playerTakeDamage;
    public UnityAction playerDead;
    public UnityAction mobDead;

    static public GlobalEvents instance;

    private void Awake()
    {
        instance = this;
    }
}
