using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform door;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            agent.SetDestination(door.position);
        }
    }
}
