using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    #region CameraRotate
    public float rotSpeed; //����

    
    private float limitMaxX = 50; //ī�޶� X�� ȸ�� ����(�ִ�)
    private float limitMinX = -12; //ī�޶� Y�� ȸ�� ����(�ּ�)

    private float limitMaxY;
    private float limitMinY;

    private float eulerAngleX; // ���콺 �� / �� �̵����� ī�޶� y�� ȸ��
    private float eulerAngleY; // ���콺 �� / �Ʒ� �̵����� ī�޶� x�� ȸ��
    #endregion

    public void CameraRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * rotSpeed;
        eulerAngleX -= mouseY * rotSpeed;
        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);
        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }

    // ī�޶� x�� ȸ���� ��� ȸ�� ������ ����
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
