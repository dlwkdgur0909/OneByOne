using UnityEngine;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using static MoveCamera;

public class mainCamera : MonoBehaviour
{
    public static mainCamera instance;
    public GameObject InteractionKey;
    public float throwForce = 10f;
    public bool isShop = false;

    #region UI
    public TMP_Text curAmmoText;
    public Image coolTimeImage;
    #endregion

    #region BulletShooter
    public GameObject flashObject;
    public GameObject flash;
    public GameObject flashPos;
    private Vector3 flashRot = new Vector3(0, 0, 0);
    public bool isHaveFlash;
    private bool isTurnOn = false;

    public GameObject gun;
    public GameObject gunOBJ;
    public GameObject gunPos;
    private Vector3 gunRot = new Vector3(-90f, 90f, 0f);

    public int maxAmmo = 15; //최대 탄약 수
    public float reloadTime = 1f; //재장전 시간
    private int curAmmo;
    private bool isReloading = false;

    //Particle
    public GameObject fireParticle;
    private ParticleSystem gunFire;
    public GameObject cannonParticle;
    private ParticleSystem cannonFire;

    public GameObject bulletPrefab;
    public GameObject cannonBallPrefab;
    public Transform curBulletPos;
    public Transform bulletPos;
    public Transform CCTVbulletPos;

    public float bulletSpeed;
    public float cannonBallSpeed;

    private float cannonCoolDown = 5f; //cannon의 쿨 타임
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
        coolTimeImage.fillAmount = 0;
        curAmmo = maxAmmo;
        gunFire = fireParticle.GetComponent<ParticleSystem>();
        cannonFire = cannonParticle.GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (isShop) return;
        if (cannonCoolTime > 0f) cannonCoolTime -= Time.deltaTime;
        Ray();
        UpdateRotate();
        HaveGun();
        HaveFlash();
    }

    public void HaveFlash()
    {
        if (!isHaveFlash) return;
        flashObject.transform.position = flashPos.transform.position;
        flashObject.transform.rotation = flashPos.transform.rotation;
        //불 껐다 켜기
        if (Input.GetMouseButtonDown(1))
        {
            isTurnOn = !isTurnOn;
            flash.GetComponent<Light>().enabled = isTurnOn;
        }
        //손전등 버리기
        if (Input.GetKeyDown(KeyCode.G))
        {
            Throw();
            isHaveFlash = false;
        }
    }

    public void HaveGun()
    {
        if (!isHaveGun) return; //총을 들고있지 않으면 리턴

        Ammo();
        gun.transform.position = gunPos.transform.position;
        gun.transform.rotation = gunPos.transform.rotation;

        //재장전
        if (Input.GetKeyDown(KeyCode.R) && curAmmo < maxAmmo)
        {
            StartCoroutine(ReLoad());
        }
        else if (curAmmo <= 0) StartCoroutine(ReLoad()); ;

        //총 발사
        if (Input.GetMouseButtonDown(1) && isReloading == false)
        {
            //cannon일 때
            if (Instance.currentState == CameraState.Tower && Shop.instance.isBuyCannon == true) Cannon();
            //CCTV를 보고있는 상태일 때 총소리 달라짐
            else if (curAmmo > 0 && Instance.isOnCamera)
            {
                --curAmmo;
                Fire();
                AudioManager.instance.cctvShoot.Play();
            }
            else if (curAmmo > 0)
            {
                --curAmmo;
                Fire();
                gunFire.Play();
                AudioManager.instance.shoot.Play();
            }
            else return;
        }
        //CCTV를 보고있는 상태이면 bulletPos를 CCTV의 bulletPos로 이동시키기
        if (MoveCamera.Instance.isOnCamera == true) curBulletPos.transform.position = CCTVbulletPos.transform.position;
        else curBulletPos.transform.position = bulletPos.transform.position;
        //총 버리기
        if (Input.GetKeyDown(KeyCode.G))
        {
            Throw();
            isHaveGun = false;
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
        if (isHaveGun) gun.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
        if (isHaveFlash) flashObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
    }

    //재장전
    IEnumerator ReLoad()
    {
        if (!isReloading)
        {
            gun.transform.DORotate(new Vector3(0, 360f, 0), reloadTime, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
            isReloading = true;
            AudioManager.instance.reload.Play();
            float reloadingTime = reloadTime;
            while (reloadingTime > 0)
            {
                reloadingTime -= Time.deltaTime;
                coolTimeImage.fillAmount = 1 - (reloadingTime / reloadTime);
                yield return null;
            }
            curAmmo = maxAmmo;
            isReloading = false;
            coolTimeImage.fillAmount = 0; // 쿨타임 이미지를 초기화
        }
        else yield return null;
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, curBulletPos.position, curBulletPos.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed * Time.deltaTime;

        Destroy(bullet, 2f);
    }

    private void Cannon()
    {
        if (cannonCoolTime <= 0)
        {
            cannonFire.Play();
            GameObject cannonBall = Instantiate(cannonBallPrefab, curBulletPos.position, curBulletPos.rotation);
            AudioManager.instance.cannonShoot.Play();
            cannonBall.GetComponent<Rigidbody>().velocity = cannonBall.transform.forward * cannonBallSpeed * Time.deltaTime;
            cannonCoolTime = cannonCoolDown;
            Destroy(cannonBall, 1.5f);
        }
    }

    public void Ray()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        // 레이캐스트가 뭔가를 감지했는지 여부를 나타내는 변수
        bool raycastHit = Physics.Raycast(ray, out hit);

        // 레이캐스트가 뭔가를 감지했을 때만 InteractionKey를 활성화
        InteractionKey.SetActive(raycastHit && (hit.transform.name == "Gun" || hit.transform.name == "Door" || hit.transform.name == "Shop" | hit.transform.name == "FlashLight"));

        if (raycastHit)
        {
            Transform objHit = hit.transform;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (objHit.name == "Gun")
                {
                    //손전등을 갖고있으면 리턴
                    if (isHaveFlash) return;
                    isHaveGun = true;
                    AudioManager.instance.reload.Play(); //총 줍는 소리로 대체함
                    gun.transform.DOMove(gunPos.transform.position, 0.2f).SetEase(Ease.OutExpo);
                    gun.transform.DORotate(gunRot, 0.2f).SetEase(Ease.OutExpo);
                }

                if (objHit.name == "FlashLight")
                {
                    //총을 갖고있으면 리턴
                    if (isHaveGun) return;
                    isHaveFlash = true;
                    flashObject.transform.DOMove(flashPos.transform.position, 0.2f).SetEase(Ease.OutExpo);
                    flashObject.transform.DORotate(flashRot, 0.2f).SetEase(Ease.OutExpo);
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

    public void Ammo()
    {
        curAmmoText.text = curAmmo.ToString() + "/15";
    }
}