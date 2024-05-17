using UnityEngine;
using static MoveCamera;

public class mainCamera : MonoBehaviour
{
    [Header("Bullet Shooter")]
    public GameObject bulletPrefab;
    public GameObject cannonBallPrefab;
    public Transform bulletPos;
    public float bulletSpeed;
    public float cannonBallSpeed;

    private float cannonCoolDown = 2f;
    private float cannonCoolTime = 0f;

    [Header("Main Camera")]
    private RotateCamera rotateToMouse;

    public bool isOnCamera = false;

    void Awake()
    {
        rotateToMouse = GetComponent<RotateCamera>();
    }

    void Update()
    {
        if (cannonCoolTime > 0f) cannonCoolTime -= Time.deltaTime;
        UpdateRotate();
        if (Input.GetMouseButtonDown(1))
        {
            if (Instance.currentState == CameraState.Tower) Cannon();
            else Fire();
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
}
