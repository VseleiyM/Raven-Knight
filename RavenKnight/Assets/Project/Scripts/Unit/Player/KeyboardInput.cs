using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] private Movement movement;

    private float horizontal;
    private float vertical;
    public Vector2 Direction => _direction;
    private Vector2 _direction;

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        _direction.Set(horizontal, vertical);
        movement.Move(_direction);
    }
}
