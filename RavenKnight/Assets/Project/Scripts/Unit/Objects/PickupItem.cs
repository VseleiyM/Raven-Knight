using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public TypePickupItem TypePickupItem => _typePickupItem;
    [SerializeField] private TypePickupItem _typePickupItem;
    public int Value => _value;
    [SerializeField] private int _value;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;

        GlobalEvents.SendItemHasPickup(this);
        animator.SetTrigger("Picked");
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
