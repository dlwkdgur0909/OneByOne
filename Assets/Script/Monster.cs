using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class Monster : MonoBehaviour
{

    NavMeshAgent agent;
    Transform door;
    public int Hp;
    public int DMG;


    private void Awake()
    { 
        agent = GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        door = GameManager.instance.door;
    }


    void Update()
    {
        agent.SetDestination(door.position);
    }
}
