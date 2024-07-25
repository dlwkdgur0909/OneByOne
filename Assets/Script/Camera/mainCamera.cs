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
    public GameObject gun;
    public GameObject gunPos;
    private Vector3 gunRot = new Vector3(-90f, 90f, 0f);

    public int maxAmmo = 15; //�ִ� ź�� ��
    public float reloadTime = 1f; //������ �ð�
    private int curAmmo;
    private bool isReloading = false;

    public GameObject bulletPrefab;
    public GameObject cannonBallPrefab;
    public Transform curBulletPos;
    public Transform bulletPos;
    public Transform CCTVbulletPos;

    public float bulletSpeed;
    public float cannonBallSpeed;

    private float cannonCoolDown = 5f; //cannon�� �� Ÿ��
    private float cannonCoolTime = 0f; //cannon�� ���� ��Ÿ�� �ð�
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
    }

    public void Update()
    {
        if (isShop == false)
        {
            if (cannonCoolTime > 0f) cannonCoolTime -= Time.deltaTime;
            Ray();
            UpdateRotate();
            HaveGun();
        }
    }

    public void HaveGun()
    {
        if (isHaveGun == true)
        {
            Ammo();
            gun.transform.position = gunPos.transform.position;
            gun.transform.rotation = gunPos.transform.rotation;
            //������
            if (Input.GetKeyDown(KeyCode.R) && curAmmo < maxAmmo && Instance.currentState != CameraState.Tower)
            {
                StartCoroutine(ReLoad());
                AudioManager.instance.reload.Play();
            }
            //�� �߻�
            if (Input.GetMouseButtonDown(1) && isReloading == false)
            {
                if (Instance.currentState == CameraState.Tower && Shop.instance.isBuyCannon == true) StartCoroutine(C_Cannon());
                else if (curAmmo > 0)
                {
                    --curAmmo;
                    Fire();
                    AudioManager.instance.shoot.Play();
                }
                else return;
            }
            if (MoveCamera.Instance.isOnCamera == true) curBulletPos.transform.position = CCTVbulletPos.transform.position;
            else curBulletPos.transform.position = bulletPos.transform.position;
            //�� ������
            if (Input.GetKeyDown(KeyCode.G))
            {
                isHaveGun = false;
                Throw();
            }
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
        float reloadingTime = reloadTime;
        while (reloadingTime > 0)
        {
            reloadingTime -= Time.deltaTime;
            coolTimeImage.fillAmount = 1 - (reloadingTime / reloadTime);
            yield return null;
        }
        curAmmo = maxAmmo;
        isReloading = false;
        coolTimeImage.fillAmount = 0; // ��Ÿ�� �̹����� �ʱ�ȭ
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, curBulletPos.position, curBulletPos.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed * Time.deltaTime;

        Destroy(bullet, 2f);
    }

    IEnumerator C_Cannon()
    {
        if (cannonCoolTime <= 0)
        {
            GameObject cannonBall = Instantiate(cannonBallPrefab, curBulletPos.position, curBulletPos.rotation);
            AudioManager.instance.cannonShoot.Play();
            yield return new WaitForSeconds(1f);
            AudioManager.instance.boom.Play();
            cannonBall.GetComponent<Rigidbody>().velocity = cannonBall.transform.forward * cannonBallSpeed * Time.deltaTime;
            cannonCoolTime = cannonCoolDown;
            Destroy(cannonBall, 1.5f);
        }
    }

    public void Ray()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        // ����ĳ��Ʈ�� ������ �����ߴ��� ���θ� ��Ÿ���� ����
        bool raycastHit = Physics.Raycast(ray, out hit);

        // ����ĳ��Ʈ�� ������ �������� ���� InteractionKey�� Ȱ��ȭ
        InteractionKey.SetActive(raycastHit && (hit.transform.name == "Gun" || hit.transform.name == "Door" || hit.transform.name == "Shop"));

        if (raycastHit)
        {
            Transform objHit = hit.transform;

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (objHit.name == "Gun")
                {
                    isHaveGun = true;
                    AudioManager.instance.reload.Play(); //�� �ݴ� �Ҹ��� ��ü��
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

    public void Ammo()
    {
        curAmmoText.text = curAmmo.ToString() + "/15";
    }
}
