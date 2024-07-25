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

    [Header("�⺻ ��")]
    public int maxHp; // �ִ� HP ��
    private int currentHp; // ���� HP ��
    public int DMG; // �빮, �÷��̾�� ���� ������
    public float maxRayDistance; // ���� ��Ÿ�
    public float coolTime; // ���� ��Ÿ��
    private float attackTimer;
    public float rotationSpeed; //ȸ�� �ӵ�
    public int goldValue; // �׿��� �� ��� ����� ��

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
        // ���� �ν����� ��
        if (frontDoor == null)
        {
            isPlayerPos = true;
            isFrontDoor = false;
        }

        // ���� �ٶ󺸰� ������ ȸ�� ����
        if (isFrontDoor && frontDoor != null)
        {
            Vector3 directionToDoor = (frontDoor.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToDoor);

            // NavMeshAgent�� ȸ���� �����ϰ� ��ǥ ȸ���� ���� ����
            transform.rotation = lookRotation;
        }

        // ������ �������� Ȯ��
        if (attackTimer < 0)
        {
            Ray();
            attackTimer = coolTime;
        }

        //�׳� ���� ��
        if (isPlayerPos) agent.SetDestination(playerPos.transform.position);
        if (isFrontDoor) agent.SetDestination(frontDoor.position);

        // ����
        if (currentHp <= 0)
        {
            Shop.instance.AddGold(goldValue);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���Ͱ� �Ѿ˿� ����� ��
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

    //����
    private void Ray()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(origin, direction, out rayHit, maxRayDistance))
        {
            //MainDoor����
            if (rayHit.transform.name == "MainDoorPos")
            {
                idleAnim.SetTrigger("isIdle");
                attackAnim.SetTrigger("isAttack");
                AudioManager.instance.monsterAttack.Play();
                rayHit.transform.GetComponent<MainDoor>().TakeDamage(DMG);
            }
            //Player ����
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
