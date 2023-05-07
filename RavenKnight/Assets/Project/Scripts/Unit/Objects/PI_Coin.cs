using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PI_Coin : PickupItem
{
    [SerializeField] private int gainScore;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GlobalEvents.SendScoreChanged(gainScore);
        GlobalEvents.SendCreateScoreText(transform.position, gainScore);
        animator.SetTrigger("Picked");
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
