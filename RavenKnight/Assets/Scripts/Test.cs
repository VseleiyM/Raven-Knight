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
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Update()
    {
        if (lookAtCursor)
        {
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            lookPos = lookPos - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (Input.GetKey(upButton)) vertical = 1;
        else if (Input.GetKey(downButton)) vertical = -1; else vertical = 0;

        if (Input.GetKey(leftButton)) horizontal = -1;
        else if (Input.GetKey(rightButton)) horizontal = 1; else horizontal = 0;

        direction = new Vector2(horizontal, vertical);


        if (horizontal > 0 && !isFacingRight) Flip(); else if (horizontal < 0 && isFacingRight) Flip(); //сробатавание Flip()
    }
}
