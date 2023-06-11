﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemMagnet : MonoBehaviour
{
    [SerializeField] private int speed = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "PickupItems") return;

        StartCoroutine(Magnet(collision.transform));
    }

    private IEnumerator Magnet(Transform target)
    {
        while (target)
        {
            target.position = Vector3.Lerp(target.position, transform.position, Time.fixedDeltaTime * speed);
            yield return new WaitForFixedUpdate();
        }
    }
}
