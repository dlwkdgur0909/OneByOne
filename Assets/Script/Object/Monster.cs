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
    public int DMG; // �빮�� ���� ������
    public float maxRayDistance; // ���� ��Ÿ�
    public float coolTime; // ���� ��Ÿ��
    private float attackTimer;
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

        // ������ �������� Ȯ��
        if (attackTimer < 0 && isFrontDoor)
        {
            DoorRay();
            attackTimer = coolTime;
        }
        if (attackTimer < 0 && isPlayerPos)
        {
            PlayerRay();
            attackTimer = coolTime;
        }

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
                hit.Play();
            }
        }
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            Bullet cannonBall = collision.gameObject.GetComponent<Bullet>();
            if (cannonBall != null)
            {
                currentHp -= cannonBall.DMG;
                hpSlider.value = currentHp;
                hit.Play();
            }
        }
    }

    //Door Attack
    private void DoorRay()
    {
        Vector3 origin = transform.position;
        Vector3 direction = frontDoor.transform.forward;

        if (Physics.Raycast(origin, direction, out rayHit, maxRayDistance))
        {
            if (rayHit.transform.name == "MainDoorPos")
            {
                Debug.Log("front door");
                AudioManager.instance.monsterAttack.Play();
                rayHit.transform.GetComponent<MainDoor>().TakeDamage(DMG);
            }
        }
    }

    //Player Attack
    private void PlayerRay()
    {
        Vector3 origin = transform.position;
        Vector3 direction = playerPos.transform.forward;

        if (Physics.Raycast(origin, direction, out rayHit, maxRayDistance))
        {
            if (rayHit.transform.name == "Player")
            {
                Debug.Log("Player");
                rayHit.transform.GetComponent<Player>().TakeDamage(DMG);
            }
        }
    }
}
