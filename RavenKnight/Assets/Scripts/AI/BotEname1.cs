using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotEname1 : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    private UnityEngine.Object explosion;

    public Transform point2;
    public Transform point;
    bool moveingRight;

    Transform Playre;
    public float stoppingDistance;
    private float stoppingDistanceSlep;

    public Transform PlayerTr;
    NavMeshAgent agent;

    bool chill = false;
    bool angry = false;
    bool goBack = false;

    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        Physics2D.queriesStartInColliders = false;
        rb = GetComponent<Rigidbody2D>();
        Playre = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        agent.SetDestination(PlayerTr.position);
        if (angry == false)
        {
            chill = true;
        }
        if (Vector2.Distance(transform.position, Playre.position) < stoppingDistance) // Можно еще подумать
        {

            angry = true;
            chill = false;
            goBack = false;
        }
        if (Vector2.Distance(transform.position, Playre.position) > stoppingDistance)
        {
            goBack = true;
            angry = false;
        }
        if (chill == true)
        {
            Chill();
        }
        else if (angry == true)
        {
            Angry();
        }
        else if (goBack == true)
        {
            GoBack();
        }
    }
    void Chill()
    {
        if (transform.position.x == point.position.x)
        {
            moveingRight = true;
        }
        else if (transform.position.x == point2.position.x)
        {
            moveingRight = false;
        }

        if (moveingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, point2.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
        }
    }

    void Angry()
    {
        agent.SetDestination(PlayerTr.position);
        transform.position = Vector2.MoveTowards(transform.position, Playre.position, speed * Time.deltaTime);
    }

    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }
}
