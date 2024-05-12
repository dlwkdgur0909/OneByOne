using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCamera : MonoBehaviour
{
    private RotateCamera rotateToMouse;

    void Awake()
    {
        rotateToMouse = GetComponent<RotateCamera>();
    }

    void Update()
    {
        UpdateRotate();
    }

    void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rotateToMouse.CameraRotate(mouseX, mouseY);
    }
}
