using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] private Movement movement;

    private float horizontal;
    private float vertical;
    private Vector2 direction;

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        direction.Set(horizontal, vertical);
        movement.Move(direction);
    }
}
