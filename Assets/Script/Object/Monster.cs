using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform door;
    private Transform frontDoor;

    public int hp;
    public int DMG;
    public int gold;

    public bool isDoor;
    public bool isFrontDoor;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void OnEnable()
    {
        door = GameManager.instance.door;
        frontDoor = GameManager.instance.frontDoor;
    }

    void Update()
    {
        if (isDoor) agent.SetDestination(door.position);
        if (isFrontDoor) agent.SetDestination(frontDoor.position);
    }
}
