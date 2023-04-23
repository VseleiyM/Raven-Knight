using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotNavMesh : MonoBehaviour
{
    public Transform playerTr;
    public NavMeshAgent agent;
    public Atac atac;

    [SerializeField] private float PositionD;

    public bool atac_sost = true;
    public bool wepon_sost = true;
    public Animator anim;
    public string AtacAnim;
    public string RanAnim;
    Vector3 Back_wepon;

    public float reternDistense;

    private float Pamyte;
    private float Pamyte_D;

    private void Awake()
    {
        
    }

    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Pamyte = agent.speed;
        Pamyte_D = agent.stoppingDistance;
    }

    void Update()
    {
        
        if (playerTr != null)
        {
            playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


            if (transform.position.x < playerTr.transform.position.x) gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (transform.position.x > playerTr.transform.position.x) gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);



            if (Vector2.Distance(transform.position, playerTr.position) <= agent.stoppingDistance)
            {
                Atac();
            }
            else if(Vector2.Distance(transform.position, playerTr.position) > agent.stoppingDistance && wepon_sost == true)
            {
                Wepon();               
            }
            else if (wepon_sost == false)
            {
                agent.speed = 0;
            }
               
        }
    }

    void Atac()
    {
        anim.Play(AtacAnim);
    }

    void Wepon()
    {
        
        if (PositionD == 0)
        {
            agent.speed = Pamyte;
            agent.SetDestination(playerTr.position);
            anim.Play(RanAnim);
            if (atac != null)
            {
                atac.Atac_Bool = false;
            }
            StartCoroutine(NextWave());
        }
        else if (PositionD == 1)
        {
            Back_wepon = new Vector3(transform.position.x, transform.position.y);
            agent.speed = Pamyte*2;
            agent.SetDestination((Back_wepon - playerTr.position));
            anim.Play(RanAnim);
            if (atac != null)
            {
                atac.Atac_Bool = false;
            }
            StartCoroutine(NextWave());
        }
        else if (PositionD == 2)
        {
            agent.speed = 0;
            anim.Play(RanAnim);
            if (atac != null)
            {
                atac.Atac_Bool = false;
            }
            StartCoroutine(NextWave());
        }
        /*else
        {

        }*/

    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(0.5f);
        PositionD = Random.Range(0, 0);
        
    }
    void AtacAnimTargit()
    {
        atac.Atac_Bool = true;
    }
}
