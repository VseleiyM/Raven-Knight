using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class BotNavMesh : MonoBehaviour
{
    public Transform playerTr;
    public NavMeshAgent agent;

    public float reternDistense;

    void Start()
    {

        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {

        if (playerTr != null)
        {
            playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            agent.SetDestination(playerTr.position);

            if (transform.position.x < playerTr.transform.position.x) gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (transform.position.x > playerTr.transform.position.x) gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);   
        }
    }
}
