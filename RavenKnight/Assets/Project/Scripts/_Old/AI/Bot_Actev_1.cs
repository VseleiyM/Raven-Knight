using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot_Actev_1 : MonoBehaviour
{
    private BotNavMesh BotMain;
    public Transform playerTr;
    private SpriteRenderer spriteRend;
    public Rigidbody2D body;

    private Material matDefault;
    public Material matBlink;

    /*
    [SerializeField] private float Distans = 4;
    [SerializeField] private float PositionD;*/
    [SerializeField] private float speed = 600;
    

    public float SpeedAttac = 4;
    public float TimeAttack = 0;
    public bool flagPamete = true;
    public Vector3 PointPosition;

    private float PamyteSpeed;
    private float PamyteStoping;

    void Start()
    {
        
        BotMain = GetComponent<BotNavMesh>();
        PamyteSpeed = BotMain.agent.speed;
        PamyteStoping = BotMain.agent.stoppingDistance;
        spriteRend = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("EnemyBlink_2", typeof(Material)) as Material;
        matDefault = spriteRend.material;
    }

    void Update()
    {
        //BotMain.agent.speed = 100;
        if (TimeAttack < Time.time && flagPamete == true)
        {
            Dash();
        }
    }

    void Dash()
    {
        flagPamete = false;
        StartCoroutine(NextWave());          
    }

    IEnumerator NextWave()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        BotMain.wepon_sost = false;
        BotMain.anim.Play("Idel");
        Vector2 PointV = new Vector2((playerTr.position.x - transform.position.x), (playerTr.position.y - transform.position.y));
        yield return new WaitForSeconds(0.5f);
        spriteRend.material = matBlink;
        BotMain.anim.Play(BotMain.RanAnim);
        body.AddForce(PointV * speed);
        yield return null;
        //transform.position = Vector2.MoveTowards(transform.position, PointV, speed * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        BotMain.wepon_sost = true;
        TimeAttack = Time.time + SpeedAttac;
        spriteRend.material = matDefault;
        BotMain.agent.speed = PamyteSpeed;
        flagPamete = true;
    }









































    /*
     public Transform playerTr;
    private SpriteRenderer spriteRend;

    private Material matDefault;
    public Material matBlink;

    [SerializeField] private float Distans = 4;
    [SerializeField] private float PositionD;
    [SerializeField] private float speed = 20;

    public float SpeedAttac = 2;
    public float TimeAttack = 0;

    void Start()
    {
        PositionD = Random.Range(5, 9);
        spriteRend = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("EnemyBlink_2", typeof(Material)) as Material;
        matDefault = spriteRend.material;
    }

    void Update()
    {

        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (Vector2.Distance(transform.position, playerTr.position) > PositionD - Distans && Vector2.Distance(transform.position, playerTr.position) < PositionD)
        {
            Dash();
        }
    }

    void Dash()
    {
        if (TimeAttack < Time.time)
        {
            //playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            transform.position = Vector2.MoveTowards(transform.position, playerTr.position, speed * Time.deltaTime);
            spriteRend.material = matBlink;
            Invoke("ResetMaterial", 0.8f);
            
        }
    }
    void ResetMaterial()
    {
        TimeAttack = Time.time + SpeedAttac;
        spriteRend.material = matDefault;
        PositionD = Random.Range(5, 9);
    }
     */



}
