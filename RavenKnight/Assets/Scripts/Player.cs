using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Speeds")]
    public float WalkSpeed = 150;
    public bool isFacingRight = true;
    public PlayerKeyboardController ControllrePlayer;

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
            if (horizontal > 0 && !isFacingRight) Flip(); else if (horizontal < 0 && isFacingRight) Flip();
            _directionState = DirectionState.Right;
        }
        horizontal = 1;
        //_animatorController.Play("Walk");

    }

    public void MoveLeft()
    {
        _moveState = MoveState.Walk;
        if (_directionState == DirectionState.Right)
        {
            if (horizontal > 0 && !isFacingRight) Flip(); else if (horizontal < 0 && isFacingRight) Flip();
            _directionState = DirectionState.Left;
        }
        horizontal = -1;
        //_animatorController.Play("Walk");
    }

    public void MoveUp()
    {
        _moveState = MoveState.Walk;
        vertical = 1;
        //_animatorController.Play("Walk");
    }

    public void MoveDoun()
    {
        _moveState = MoveState.Walk;
        vertical = -1;
        //_animatorController.Play("Walk");
    }

    void Flip()// готово
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        if (theScale.x > 0) _directionState = DirectionState.Right;
        else _directionState = DirectionState.Left;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void Idle()
    {
        _moveState = MoveState.Idle;
        //_animatorController.Play("Idle");
    }

    private void Start()
    {
        ControllrePlayer = GetComponent<PlayerKeyboardController>();
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

        if(ControllrePlayer.ClikLock == true)
        {
            horizontal = 0;
            vertical = 0;
        }
        direction = new Vector2(horizontal, vertical);
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
