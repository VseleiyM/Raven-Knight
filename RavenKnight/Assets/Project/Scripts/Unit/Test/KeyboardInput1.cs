using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput1 : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private DashAbilitys dashAbilitys;

    private float horizontal;
    private float vertical;
    public Vector2 Direction => _direction;
    private Vector2 _direction;
    public KeyCode abilityKey = KeyCode.Space; // Added ability key

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        _direction.Set(horizontal, vertical);
        if (Input.GetKeyDown(abilityKey))
        {
            dashAbilitys.ActiveDash(_direction);
        }
    }

    private void FixedUpdate()
    {
        movement.Move(_direction);
    }
}
