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
    Transform mainDoor;
    public int Hp;
    public int DMG;

    public bool isNomal;
    public bool isSpecial;

    private void Awake()
    { 
        agent = GetComponent<NavMeshAgent>();
    }

    void OnEnable()
    {
        door = GameManager.instance.door;
        mainDoor = GameManager.instance.mainDoor;
    }

    void Update()
    {
        if(isNomal) agent.SetDestination(door.position);
        if (isSpecial) agent.SetDestination(mainDoor.position);
    }
}
