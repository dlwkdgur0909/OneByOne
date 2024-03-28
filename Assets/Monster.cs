using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 door = new Vector3(0, 5, -75);
    public float speed = 10f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, door, speed);
        agent.SetDestination(door);
    }
}
