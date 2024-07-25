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
    public int DMG; //�빮�� ���� ������
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
        //���Ͱ� �Ѿ˿� ����� ��
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
