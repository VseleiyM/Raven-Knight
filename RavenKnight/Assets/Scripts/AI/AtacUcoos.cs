using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AtacUcoos : MonoBehaviour
{
    public GameObject round;
    public Transform transformSootPoint;
    public NavMeshAgent agent;

    public float SpeedAttac;
    [SerializeField] private float TimeAttack = 0;

    public float offset;
    private float LoclSx;
    private float LoclSy;
    private float LoclSz;

    public Transform playerTr;
    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        LoclSx = transform.localScale.x;
        LoclSy = transform.localScale.y;
        LoclSz = transform.localScale.z;
    }

    void Update()
    {


        

        if(playerTr != null)
        {

            playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            Vector3 lookPos = playerTr.transform.position;
            lookPos = lookPos - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 LoclScale = new Vector3(LoclSx, LoclSy, LoclSz);

            if (angle > 90 || angle < -90)
            {
                LoclScale.y = -LoclSy;
            }
            else
            {
                LoclScale.y = +LoclSy;
            }

            transform.localScale = LoclScale;

            if(Vector2.Distance(transform.position, playerTr.position) < agent.stoppingDistance + 3)
            {
                if (TimeAttack < Time.time)
                {
                  GameObject spawnedRound = Instantiate(
                            round,
                            transformSootPoint.position,
                            transform.rotation);  
                    TimeAttack = Time.time + SpeedAttac;
                }
            }
            
            
        }
        
    }
}
