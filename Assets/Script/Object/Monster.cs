using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform door;
    private Transform frontDoor;
    private Vector3 playerPos;

    public int hp;
    public int DMG; //대문에 넣을 데미지
    public int goldValue;

    public bool isDoor;
    public bool isFrontDoor;
    public bool isPlayer; 

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void OnEnable()
    {
        door = GameManager.instance.door;
        frontDoor = GameManager.instance.frontDoor;
        playerPos = MoveCamera.Instance.transform.position;
    }

    void Update()
    {
        if (isDoor) agent.SetDestination(door.position);
        if (isFrontDoor) agent.SetDestination(frontDoor.position);
        if(isPlayer) agent.SetDestination(playerPos);

        //Die
        if (hp < 0)
        {
            Shop.instance.AddGold(goldValue);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //몬스터가 총알에 닿았을 때
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if(bullet != null) 
            {
                hp -= bullet.DMG;
            }
        }
    }
}
