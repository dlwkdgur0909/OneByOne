using DG.Tweening;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public NavMeshAgent agent;
    RaycastHit rayHit;
    public Slider hpSlider;

    public Animator idleAnim;
    public Animator attackAnim;

    #region door
    private Transform playerPos;
    private Transform frontDoor;
    private bool isPlayerPos = false;
    private bool isFrontDoor = true;
    #endregion

    public GameObject hitParticle;
    private ParticleSystem hit;

    [Header("기본 값")]
    public int maxHp; // 최대 HP 값
    private int currentHp; // 현재 HP 값
    public int DMG; // 대문, 플레이어에게 넣을 데미지
    public float maxRayDistance; // 공격 사거리
    public float coolTime; // 공격 쿨타임
    private float attackTimer;
    public float rotationSpeed; //회전 속도
    public int goldValue; // 죽였을 때 얻는 골드의 양

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void OnEnable()
    {
        playerPos = GameManager.instance.playerPos;
        frontDoor = GameManager.instance.frontDoor;
        hit = hitParticle.GetComponent<ParticleSystem>();
    }

    void Start()
    {
        currentHp = maxHp;
        hpSlider.maxValue = maxHp;
        hpSlider.value = currentHp;
    }

    void Update()
    {
        attackTimer -= Time.deltaTime;
        // 문이 부숴졌을 때
        if (frontDoor == null)
        {
            isPlayerPos = true;
            isFrontDoor = false;
        }

        // 문을 바라보게 몬스터의 회전 조정
        if (isFrontDoor && frontDoor != null)
        {
            Vector3 directionToDoor = (frontDoor.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToDoor);

            // NavMeshAgent의 회전을 무시하고 목표 회전을 직접 설정
            transform.rotation = lookRotation;
        }

        // 공격이 가능한지 확인
        if (attackTimer < 0)
        {
            Ray();
            attackTimer = coolTime;
        }

        //그냥 걸을 때
        if (isPlayerPos) agent.SetDestination(playerPos.transform.position);
        if (isFrontDoor) agent.SetDestination(frontDoor.position);

        // 죽음
        if (currentHp <= 0)
        {
            Shop.instance.AddGold(goldValue);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 몬스터가 총알에 닿았을 때
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                currentHp -= bullet.DMG;
                hpSlider.value = currentHp;
                hit.Play(); //particle
            }
        }
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            Bullet cannonBall = collision.gameObject.GetComponent<Bullet>();
            if (cannonBall != null)
            {
                currentHp -= cannonBall.DMG;
                hpSlider.value = currentHp;
                hit.Play(); //particle
            }
        }
    }

    //공격
    private void Ray()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(origin, direction, out rayHit, maxRayDistance))
        {
            //MainDoor감지
            if (rayHit.transform.name == "MainDoorPos")
            {
                idleAnim.SetTrigger("isIdle");
                attackAnim.SetTrigger("isAttack");
                AudioManager.instance.monsterAttack.Play();
                rayHit.transform.GetComponent<MainDoor>().TakeDamage(DMG);
            }
            //Player 감지
            if (rayHit.transform.name == "Player")
            {
                Debug.Log("Player");
                idleAnim.SetTrigger("isIdle");
                attackAnim.SetTrigger("isAttack");
                rayHit.transform.GetComponent<Player>().TakeDamage(DMG);
            }
        }
    }
}
