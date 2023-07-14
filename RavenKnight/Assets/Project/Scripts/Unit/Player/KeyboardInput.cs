using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private DashAbilitys dashAbilitys;
    [SerializeField] private KeyCode abilityKey = KeyCode.Space;

    private float horizontal;
    private float vertical;
    public Vector2 Direction => _direction;
    private Vector2 _direction;

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        _direction.Set(horizontal, vertical);

        movement.LookDirection(_direction);
        if (Input.GetKeyDown(abilityKey))
            dashAbilitys.TakeDash(_direction);
    }

    private void FixedUpdate()
    {
        movement.Move(_direction);
    }
}
