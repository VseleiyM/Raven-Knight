using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [Space(10)]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        Init();
        Flip();

        void Init()
        {
            if (direction.magnitude > 0)
                animator.SetBool("Run", true);
            else
                animator.SetBool("Run", false);

            Vector2 offset = direction * Time.fixedDeltaTime * speed;
            _rigidbody.MovePosition(_rigidbody.position + offset);
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
