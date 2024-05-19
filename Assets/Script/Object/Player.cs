using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;

    void Update()
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

            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            Quaternion cameraRotation = Quaternion.LookRotation(cameraForward);
            dir = cameraRotation * dir;

            transform.Translate(dir * speed * Time.deltaTime, Space.World);
        }
    }
}