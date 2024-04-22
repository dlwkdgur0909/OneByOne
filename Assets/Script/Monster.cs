using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class Monster : MonoBehaviour
{
    public static Monster instance { get; private set; }

    public NavMeshAgent agent;
    Transform door;
    Transform mainDoor;

    public bool toDoor;
    public bool toMainDoor;

    public int Hp;
    public int DMG;

    private void Awake()
    {
        if (instance == null) instance = this; else { Destroy(gameObject); }
        agent = GetComponent<NavMeshAgent>();
    }

    public void OnEnable()
    {
        door = GameManager.instance.door;
        mainDoor = GameManager.instance.mainDoor;
    }

    void Update()
    {
        if (toDoor) agent.SetDestination(door.position);
        if (toMainDoor) agent.SetDestination(mainDoor.position);
    }
}
