using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;

    private bool isMoving = false;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (MoveCamera.Instance.isOnCamera == false)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 dir = new Vector3(h, 0f, v).normalized;

            if (dir != Vector3.zero && !isMoving)
            {
                AudioManager.instance.walking.Play();
                isMoving = true;
            }
            else if (dir == Vector3.zero && isMoving)
            {
                AudioManager.instance.walking.Stop();
                isMoving = false;
            }

            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            Quaternion cameraRotation = Quaternion.LookRotation(cameraForward);
            dir = cameraRotation * dir;
            transform.Translate(dir * speed * Time.deltaTime, Space.World);
        }
        else
        {
            if (isMoving)
            {
                AudioManager.instance.walking.Stop();
                isMoving = false;
            }
        }
    }
}
