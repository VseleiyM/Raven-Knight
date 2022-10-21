using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFlip : MonoBehaviour
{
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public float speed = 1.5f;
    public Camera mainCamera;
    public bool facingRight = true; // на старте, персонаж смотрит вправо?
    private Vector3 theScale;
    private Rigidbody2D body;
    private Vector3 pos;
    private float h;
    private bool FlipChakc;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        theScale = transform.localScale;
    }

    void FixedUpdate()
    {
        // добавляем ускорение
        body.AddForce(Vector2.right * h * speed * body.mass * 100);

        if (Mathf.Abs(body.velocity.x) > speed) // если скорость тела превышает установленную, то выравниваем ее
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed, body.velocity.y);
    }

    // разворот относительно позиции курсора
    void LookAtCursor()
    {
        if (Input.mousePosition.x < pos.x && facingRight) FlipChakc = true;
        else if (Input.mousePosition.x > pos.x && !facingRight) FlipChakc = false;
    }

    void Update()
    {
        if (Input.GetKey(left)) h = -1; else if (Input.GetKey(right)) h = 1; else h = 0;

        if (Input.GetMouseButton(1)) // если нажата ПКМ, персонаж будет двигаться в сторону курсора
            if (facingRight) h = 1; else h = -1;

        // переносим позицию из мировых координат в экранные
        pos = mainCamera.WorldToScreenPoint(transform.position);

        if (h == 0) LookAtCursor();

        if (!Input.GetMouseButton(0)) // если ЛКМ (стрельба) не нажата, разворот по вектору движения
        {
            if (h > 0 && !facingRight) FlipChakc = false; else if (h < 0 && facingRight) FlipChakc = true;
        }
        else
        {
            LookAtCursor();
        }
        Flip();
    }

    void Flip() // отразить по горизонтали
    {
        facingRight = !facingRight;
        /*theScale.x *= -1;
        transform.localScale = theScale;*/
        if (FlipChakc == false)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(FlipChakc == true)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
