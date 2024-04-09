using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class Monster : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform door;
    public int Hp;
    public int DMG;
    

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
