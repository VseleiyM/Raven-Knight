using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Speeds")]
    public float WalkSpeed = 150;
    public bool isFacingRight = true;

    private MoveState _moveState = MoveState.Idle;
    private DirectionState _directionState = DirectionState.Right;
    private Transform _transform;
    private Rigidbody2D body;
    private Animator _animatorController;
    private float _walkTime = 0, _walkCooldown = 0.2f;
    private Vector3 direction;
    private float vertical;
    private float horizontal;

    public void MoveRight()
    {
        _moveState = MoveState.Walk;
        if (_directionState == DirectionState.Left)
        {
            _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
            _directionState = DirectionState.Right;
        }
        _walkTime = _walkCooldown;
        _animatorController.Play("Walk");
   
    }

    public void MoveLeft()
    {
            _moveState = MoveState.Walk;
            if (_directionState == DirectionState.Right)
            {
                _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
                _directionState = DirectionState.Left;
            }
            _walkTime = _walkCooldown;
            _animatorController.Play("Walk");
        
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void Idle()
    {
        _moveState = MoveState.Idle;
        _animatorController.Play("Idle");
    }

    private void Start()
    {
        _transform = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        body.fixedAngle = true;
        _animatorController = GetComponent<Animator>();
        _directionState = transform.localScale.x > 0 ? DirectionState.Right : DirectionState.Left;

        body.gravityScale = 0;
        body.drag = 10;
    }

    private void Update()
    {
        if (horizontal > 0 && !isFacingRight) Flip(); else if (horizontal < 0 && isFacingRight) Flip();
        if (_moveState == MoveState.Walk)
        {
            body.velocity = ((_directionState == DirectionState.Right ? Vector2.right : -Vector2.right)
                                    * WalkSpeed * Time.deltaTime);
            _walkTime -= Time.deltaTime;
            if (_walkTime <= 0)
            {
                Idle();
            }
        }
    }

    void FixedUpdate()
    {
        body.AddForce(direction * body.mass * WalkSpeed);

        if (Mathf.Abs(body.velocity.x) > WalkSpeed / 100f)
        {
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * WalkSpeed / 100f, body.velocity.y);
        }

        if (Mathf.Abs(body.velocity.y) > WalkSpeed / 100f)
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Sign(body.velocity.y) * WalkSpeed / 100f);
        }

    }

    enum DirectionState
    {
        Right,
        Left
    }

    enum MoveState
    {
        Idle,
        Walk,
    }
}
