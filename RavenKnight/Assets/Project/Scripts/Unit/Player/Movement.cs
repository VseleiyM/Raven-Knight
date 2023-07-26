using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInfo))]

public class Movement : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private Rigidbody2D _rigidbody;
    private MousePosition mousePosition;
    private Vector2 normal;

    public bool isFiring;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        playerInfo = GetComponent<PlayerInfo>();
        mousePosition = GetComponent<MousePosition>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        normal = collision.contacts[collision.contacts.Length - 1].normal;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        normal = Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)normal * 3);
    }

    public void Move(Vector2 direction)
    {
        Vector2 offset = Project(direction) * Time.fixedDeltaTime * playerInfo.Player.Speed;
        _rigidbody.MovePosition(_rigidbody.position + offset);

        playerInfo.Animator.SetBool("Run", direction.magnitude > 0);
    }

    public void LookDirection(Vector2 lookDir)
    {
        if (isFiring)
        {
            playerInfo.SpriteRenderer.flipX = mousePosition.LookVector.x < 0;
        }
        else
        {
            if (lookDir.x < 0)
                playerInfo.SpriteRenderer.flipX = true;
            else if (lookDir.x > 0)
                playerInfo.SpriteRenderer.flipX = false;
        }
    }

    private Vector2 Project(Vector2 direction)
    {
        
        if (Vector2.Dot(direction, normal) > 0)
        {
            return direction;
        }
        else
        {
            return (direction - Vector2.Dot(direction, normal) * normal).normalized;
        }
    }
}
