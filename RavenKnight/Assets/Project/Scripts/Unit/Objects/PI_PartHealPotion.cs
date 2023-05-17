using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PI_PartHealPotion : MonoBehaviour
{
    [SerializeField] [Min(0)] private int points;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        if (UI_HealPotion.instance != null)
            UI_HealPotion.instance.OnItemPickup(points);
        animator.SetTrigger("Picked");
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
