using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    #region CameraRotate
    public float rotSpeed; //감도

    
    private float limitMaxX = 50; //카메라 X축 회전 범위(최대)
    private float limitMinX = -12; //카메라 Y축 회전 범위(최소)

    private float limitMaxY;
    private float limitMinY;

    private float eulerAngleX; // 마우스 좌 / 우 이동으로 카메라 y축 회전
    private float eulerAngleY; // 마우스 위 / 아래 이동으로 카메라 x축 회전
    #endregion

    public void CameraRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * rotSpeed;
        eulerAngleX -= mouseY * rotSpeed;
        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);
        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }

    // 카메라 x축 회전의 경우 회전 범위를 설정
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }

        if (angle > 360)
        {
            angle -= 360;
        }

        return Mathf.Clamp(angle, min, max);
    }
}
