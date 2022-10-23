using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public float speed = 150;
    public bool lookAtCursor;
    public KeyCode leftButton = KeyCode.A;
    public KeyCode rightButton = KeyCode.D;
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;
    public bool isFacingRight = true;
    private Vector3 direction;
    private float vertical;
    private float horizontal;
    private Rigidbody2D body;

    //public bool facingRight = true;
    public Camera mainCamera;
    [Range(0f, 100f)] public float flipBufR = 10;
    [Range(0f, 100f)] public float flipBufL = 35;

    private bool FlipChakc;
    private Vector3 pos;

    void Start()
    {

        body = GetComponent<Rigidbody2D>();
        body.fixedAngle = true;

        body.gravityScale = 0;
        body.drag = 10;

    }

    void FixedUpdate()
    {
        body.AddForce(direction * body.mass * speed);

        if (Mathf.Abs(body.velocity.x) > speed / 100f)
        {
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed / 100f, body.velocity.y);
        }

        if (Mathf.Abs(body.velocity.y) > speed / 100f)
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Sign(body.velocity.y) * speed / 100f);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        if (direction != null)
        {
            if (FlipChakc == false)gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            else if(FlipChakc == true)gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }


        /*Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;*/
    }
    /*void LookAtCursor()
    {
        if (Input.mousePosition.x < pos.x && facingRight) Flip();
        else if (Input.mousePosition.x > pos.x && !facingRight) Flip();
    }*/

    void Update()
    {

        if (Input.GetKey(upButton)) vertical = 1;
        else if (Input.GetKey(downButton)) vertical = -1; else vertical = 0;

        if (Input.GetKey(leftButton)) horizontal = -1;
        else if (Input.GetKey(rightButton)) horizontal = 1; else horizontal = 0;

        direction = new Vector2(horizontal, vertical);

        pos = mainCamera.WorldToScreenPoint(transform.position);
        //условие разварота
        if (Input.mousePosition.x + flipBufL < pos.x && isFacingRight) FlipChakc = true;
        else if (Input.mousePosition.x - flipBufR > pos.x && !isFacingRight)FlipChakc = false;

        Flip();

        /*if (horizontal == 0) LookAtCursor();

        if (!Input.GetMouseButton(0)) // если ЛКМ (стрельба) не нажата, разворот по вектору движения
        {
            if (horizontal > 0 && !facingRight) Flip(); else if (horizontal < 0 && facingRight) Flip();
        }
        else
        {
            LookAtCursor();
        }*/

        //if (horizontal > 0 && !isFacingRight) Flip(); else if (horizontal < 0 && isFacingRight)Flip(); //сробатавание Flip()
    }
}
