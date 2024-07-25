using UnityEngine;
using DG.Tweening;
using static MoveCamera;

public class mainCamera : MonoBehaviour
{
    public static mainCamera instance;

    #region BulletShooter
    [Header("Bullet Shooter")]
    public GameObject gun;
    public GameObject gunPos;
    private Vector3 gunRot = new Vector3(-90f, 90f, 0f);
    public GameObject bulletPrefab;
    public GameObject cannonBallPrefab;
    public Transform bulletPos;
    public float bulletSpeed;
    public float cannonBallSpeed;
    private float cannonCoolDown = 2f; //cannon�� �� Ÿ��
    private float cannonCoolTime = 0f; //cannon�� ���� ��Ÿ�� �ð�
    private bool isHaveGun = false;
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

    void Update()
    {
        Ray();
        UpdateRotate();
        if (cannonCoolTime > 0f) cannonCoolTime -= Time.deltaTime;

        if (isHaveGun == true)
        {
            gun.transform.position = gunPos.transform.position;
            gun.transform.rotation = gunPos.transform.rotation;
            if (Input.GetMouseButtonDown(1))
            {
                if (Instance.currentState == CameraState.Tower) Cannon();
                else Fire();
            }
        }
    }

    void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rotateToMouse.CameraRotate(mouseX, mouseY);
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objHit = hit.transform;

                //ī�޶� �ٶ󺸴� ������Ʈ�� Gun�̰� EŰ�� ������ ��
                if (objHit.name == "Gun")
                {
                    isHaveGun = true;
                    gun.transform.DOMove(gunPos.transform.position, 0.5f).SetEase(Ease.OutExpo);
                    gun.transform.DORotate(gunRot, 0.5f).SetEase(Ease.OutExpo);
                }

                //���� �������� �� EŰ�� ������ ���� ���� 
                if (objHit.name == "Door")
                {
                    objHit.GetComponent<Door>().ChangeIsOpen();
                }
            }
        }
    }
}
