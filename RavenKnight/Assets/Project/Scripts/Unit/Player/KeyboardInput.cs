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

    private float pressDirX;
    private float pressDirY;
    private Vector2 pressDir;

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        _direction.Set(horizontal, vertical);

        pressDirX = 0;
        pressDirY = 0;
        if (Input.GetKeyDown(KeyCode.W))
        {
            pressDirY = 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            pressDirY = -1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pressDirX = 1;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            pressDirX = -1;
        }
        pressDir.Set(pressDirX, pressDirY);
    }

    private void FixedUpdate()
    {
        movement.Move(_direction, pressDir);
    }
}
