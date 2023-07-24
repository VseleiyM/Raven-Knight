using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private Player player;
    [SerializeField] private DashAbilitys dashAbilitys;
    [SerializeField] private KeyCode abilityKey = KeyCode.Space;
    [SerializeField] private KeyCode invincible = KeyCode.I;

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

        if (Input.GetKeyDown(invincible))
            player.ChangeInvincible();
    }

    private void FixedUpdate()
    {
        movement.Move(_direction);
    }
}
