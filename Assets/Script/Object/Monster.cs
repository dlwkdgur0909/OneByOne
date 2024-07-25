using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public NavMeshAgent agent;
    public Slider hpSlider;

    private Transform door;
    private Transform frontDoor;

    public GameObject hitParticle;
    private ParticleSystem hit;

    [Header("�⺻ ��")]
    public int maxHp; // �ִ� HP ��
    public int DMG; // �빮�� ���� ������
    public int goldValue;

    private int currentHp; // ���� HP ��

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

    void Start()
    {
        currentHp = maxHp;
        hpSlider.maxValue = maxHp;
        hpSlider.value = currentHp;
        hit = hitParticle.GetComponent<ParticleSystem>();   
    }

    void Update()
    {
        if (isDoor) agent.SetDestination(door.position);
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
}
