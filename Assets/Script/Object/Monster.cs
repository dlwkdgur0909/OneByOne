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

    [Header("기본 값")]
    public int maxHp; // 최대 HP 값
    private int currentHp; // 현재 HP 값
    public int DMG; // 대문에 넣을 데미지
    public float maxRayDistance; //공격 사거리
    public float coolTime; //공격 쿨타임
    public int goldValue; //죽였을 때 얻는 골드의 양


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
        //if (isDoor) agent.SetDestination(door.position);
        if (isFrontDoor) agent.SetDestination(frontDoor.position);
        Ray();

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
            if(rayHit.transform.name == "Main Door Pos")
            {
                StartCoroutine(Attack(coolTime));
                Debug.Log("Attack Door");
            }
        }
    }

    IEnumerator Attack(float coolTime)
    {
        rayHit.transform.GetComponent<MainDoor>().TakeDamage(DMG);
        yield return new WaitForSeconds(coolTime);
    }
}
