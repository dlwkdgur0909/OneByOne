using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public NavMeshAgent agent;
    
    private Transform door;
    private Transform frontDoor;
    private Vector3 playerPos;

    [Header("�⺻ ��")]
    public int hp;
    public int DMG; //�빮�� ���� ������
    public int goldValue;

    public bool isDoor;
    public bool isFrontDoor;

    // �߰��� ����
    private bool isAttacking;

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
        if (isAttacking) return; // �̹� ���� ���̸� ������Ʈ�� ����

        if (isDoor) agent.SetDestination(door.position);
        if (isFrontDoor) agent.SetDestination(frontDoor.position);

        // Door�� �������� �� ����
        if (isDoor && Vector3.Distance(transform.position, door.position) < 1.5f)
        {
            AttackDoor(door);
        }
        // FrontDoor�� �������� �� ����
        if (isFrontDoor && Vector3.Distance(transform.position, frontDoor.position) < 1.5f)
        {
            AttackDoor(frontDoor);
        }

        //Die
        if (hp <= 0)
        {
            Shop.instance.AddGold(goldValue);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //���Ͱ� �Ѿ˿� ����� ��
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                hp -= bullet.DMG;
            }
        }
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            Bullet cannonBall = collision.gameObject.GetComponent<Bullet>();
            if (cannonBall != null)
            {
                hp -= cannonBall.DMG;
            }
        }
    }

    private void AttackDoor(Transform targetDoor)
    {
        isAttacking = true;
        agent.isStopped = true; // NavMesh ����

        // Raycast �߻�
        RaycastHit hit;
        if (Physics.Raycast(transform.position, targetDoor.position - transform.position, out hit, Mathf.Infinity))
        {
            // ���� ������ ������� Ȯ��
            if (hit.transform.CompareTag("Door") || hit.transform.CompareTag("FrontDoor"))
            {
                hit.transform.GetComponent<MainDoor>().TakeDamage(DMG);
            }
        }

        // ���� �� ���� �ð� ��� �� �ٽ� �̵�
        Invoke(nameof(ResumeMovement), 1f);
    }

    private void ResumeMovement()
    {
        isAttacking = false;
        agent.isStopped = false; // NavMesh �̵� �簳
    }
}
