using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public NavMeshAgent agent;
    
    private Transform door;
    private Transform frontDoor;
    private Vector3 playerPos;

    [Header("기본 값")]
    public int hp;
    public int DMG; //대문에 넣을 데미지
    public int goldValue;

    public bool isDoor;
    public bool isFrontDoor;

    // 추가된 변수
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
        if (isAttacking) return; // 이미 공격 중이면 업데이트를 중지

        if (isDoor) agent.SetDestination(door.position);
        if (isFrontDoor) agent.SetDestination(frontDoor.position);

        // Door에 도착했을 때 공격
        if (isDoor && Vector3.Distance(transform.position, door.position) < 1.5f)
        {
            AttackDoor(door);
        }
        // FrontDoor에 도착했을 때 공격
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
        //몬스터가 총알에 닿았을 때
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
        agent.isStopped = true; // NavMesh 중지

        // Raycast 발사
        RaycastHit hit;
        if (Physics.Raycast(transform.position, targetDoor.position - transform.position, out hit, Mathf.Infinity))
        {
            // 공격 가능한 대상인지 확인
            if (hit.transform.CompareTag("Door") || hit.transform.CompareTag("FrontDoor"))
            {
                hit.transform.GetComponent<MainDoor>().TakeDamage(DMG);
            }
        }

        // 공격 후 일정 시간 대기 후 다시 이동
        Invoke(nameof(ResumeMovement), 1f);
    }

    private void ResumeMovement()
    {
        isAttacking = false;
        agent.isStopped = false; // NavMesh 이동 재개
    }
}
