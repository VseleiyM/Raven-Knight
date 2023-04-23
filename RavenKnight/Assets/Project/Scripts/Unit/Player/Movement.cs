using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInfo))]

public class Movement : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private Rigidbody2D _rigidbody;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();
        animator = playerInfo.Animator;
        spriteRenderer = playerInfo.SpriteRenderer;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        Init();
        Animation();
        Flip();

        void Init()
        {
            Vector2 offset = direction * Time.fixedDeltaTime * playerInfo.Player.Speed;
            _rigidbody.MovePosition(_rigidbody.position + offset);
        }

        void Animation()
        {
            if (direction.magnitude > 0)
                animator.SetBool("Run", true);
            else
                animator.SetBool("Run", false);
        }

        void Flip()
        {
            if (direction.x < 0)
                spriteRenderer.flipX = true;
            else if (direction.x > 0)
                spriteRenderer.flipX = false;
        }
    }
}
