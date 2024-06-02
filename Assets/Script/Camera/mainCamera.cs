using UnityEngine;
using DG.Tweening;
using static MoveCamera;
using System.Collections;

public class mainCamera : MonoBehaviour
{
    public static mainCamera instance;
    public GameObject InteractionKey;
    public float throwForce = 10f;
    public bool isShop = false;

    #region BulletShooter
    [Header("Bullet Shooter")]
    public GameObject gun;
    public GameObject gunPos;
    private Vector3 gunRot = new Vector3(-90f, 90f, 0f);

    public int maxAmmo = 15; //최대 탄약 수
    public float reloadTime = 1f; //재장전 시간
    private int curAmmo;
    private bool isReloading = false;

    public GameObject bulletPrefab;
    public GameObject cannonBallPrefab;
    public Transform bulletPos; // 2,-0.9, 7.07 / 0, -1.2, 0.3

    public float bulletSpeed;
    public float cannonBallSpeed;

    private float cannonCoolDown = 2f; //cannon의 쿨 타임
    private float cannonCoolTime = 0f; //cannon의 현재 쿨타임 시간
    public bool isHaveGun = false;
    #endregion 

    [Header("Main Camera")]
    private RotateCamera rotateToMouse;

    [Header("Raycast")]
    public new Camera camera;
    RaycastHit hit;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        rotateToMouse = GetComponent<RotateCamera>();
    }

    private void Start()
    {
        curAmmo = maxAmmo;
    }

    public void Update()
    {
        if (isShop == false)
        {
            Ray();
            UpdateRotate();
            if (cannonCoolTime > 0f) cannonCoolTime -= Time.deltaTime;

            if (isHaveGun == true)
            {
                gun.transform.position = gunPos.transform.position;
                gun.transform.rotation = gunPos.transform.rotation;
                if (Input.GetKeyDown(KeyCode.R))
                {
                    StartCoroutine(ReLoad());
                }
                if (Input.GetMouseButtonDown(1))
                {
                    if (Instance.currentState == CameraState.Tower) Cannon();
                    else if (curAmmo > 0) { --curAmmo; Fire(); }
                    else return;
                }
            }
        }
        //총 버리기
        if (Input.GetKeyDown(KeyCode.G))
        {
            isHaveGun = false;
            Throw();
        }
    }

    private void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rotateToMouse.CameraRotate(mouseX, mouseY);
    }

    private void Throw()
    {
        gun.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
    }

    IEnumerator ReLoad()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        curAmmo = maxAmmo;
        isReloading = false;
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed * Time.deltaTime;

        Destroy(bullet, 2f);
    }

    private void Cannon()
    {
        if (cannonCoolTime <= 0)
        {
            GameObject cannonBall = Instantiate(cannonBallPrefab, bulletPos.position, bulletPos.rotation);
            cannonBall.GetComponent<Rigidbody>().velocity = cannonBall.transform.forward * cannonBallSpeed * Time.deltaTime;
            cannonCoolTime = cannonCoolDown;
            Destroy(cannonBall, 3f);
        }
    }

    public void Ray()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        // 레이캐스트가 뭔가를 감지했는지 여부를 나타내는 변수
        bool raycastHit = Physics.Raycast(ray, out hit);

        // 레이캐스트가 뭔가를 감지했을 때만 InteractionKey를 활성화
        InteractionKey.SetActive(raycastHit && (hit.transform.name == "Gun" || hit.transform.name == "Door" || hit.transform.name == "Shop"));

        if (raycastHit)
        {
            Transform objHit = hit.transform;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (objHit.name == "Gun")
                {
                    isHaveGun = true;
                    gun.transform.DOMove(gunPos.transform.position, 0.2f).SetEase(Ease.OutExpo);
                    gun.transform.DORotate(gunRot, 0.2f).SetEase(Ease.OutExpo);
                }

                if (objHit.name == "Door")
                {
                    objHit.GetComponent<Door>().ChangeIsOpen();
                }

                if (objHit.name == "Shop")
                {
                    objHit.GetComponent<Shop>().OpenShop();
                    isShop = true;
                }
            }
        }
    }
}
