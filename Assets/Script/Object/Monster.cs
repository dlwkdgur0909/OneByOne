using DG.Tweening;
using System.Collections;
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
    //private Transform door;
    private Transform frontDoor;
    //public bool isDoor;
    public bool isFrontDoor;
    #endregion

    public GameObject hitParticle;
    private ParticleSystem hit;

    [Header("�⺻ ��")]
    public int maxHp; // �ִ� HP ��
    private int currentHp; // ���� HP ��
    public int DMG; // �빮�� ���� ������
    public float maxRayDistance; //���� ��Ÿ�
    public float coolTime; //���� ��Ÿ��
    private float attackTimer;
    public int goldValue; //�׿��� �� ��� ����� ��


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void OnEnable()
    {
        //door = GameManager.instance.door;
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

        //������ �������� Ȯ��
        if (attackTimer < 0)
        {
            Ray();
            attackTimer = coolTime;
        }

        //if (isDoor) agent.SetDestination(door.position);
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

    public void Ray()
    {
        Debug.DrawRay(transform.position, transform.forward * maxRayDistance, Color.red, 0.1f);

        if (Physics.Raycast(transform.position, transform.forward, out rayHit, maxRayDistance))
        {
            if(rayHit.transform.name == "MainDoorPos")
            {
                rayHit.transform.GetComponent<MainDoor>().TakeDamage(DMG);
            }
        }
    }
}
