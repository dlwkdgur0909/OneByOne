using UnityEngine;
using static MoveCamera;

public class RotateCamera : MonoBehaviour
{
    public Transform[] curCameraObj;
    #region CameraRotate
    public float rotSpeed; //����

    private float limitMaxY = 50; //ī�޶� Y�� ȸ�� ����(�ִ�)
    private float limitMinY = -17; //ī�޶� Y�� ȸ�� ����(�ּ�)

    private float eulerAngleX; // ���콺 �� / �� �̵����� ī�޶� y�� ȸ��
    private float eulerAngleY; // ���콺 �� / �Ʒ� �̵����� ī�޶� x�� ȸ��
    #endregion

    public void CameraRotate(float mouseX, float mouseY)
    {
        transform.rotation = Quaternion.Euler(eulerAngleY, 180, 0);
        eulerAngleY += mouseX * rotSpeed;
        eulerAngleX -= mouseY * rotSpeed;
        eulerAngleX = ClampAngle(eulerAngleX, limitMinY, limitMaxY);

        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
        if (curCameraObj != null)
        {
            switch (Instance.currentState)
            {
                case CameraState.Center:
                    {
                        ChoiceCamera(CameraState.Center); break;
                    }
                case CameraState.Right:
                    {
                        ChoiceCamera(CameraState.Right); break;
                    }
                case CameraState.Left:
                    {
                        ChoiceCamera(CameraState.Left); break;
                    }
                case CameraState.Tower:
                    {
                        ChoiceCamera(CameraState.Tower); break;
                    }
            }
        }
    }

    public void ChoiceCamera(CameraState cameraIndex)
    {
        for (CameraState i = CameraState.Center; i <= CameraState.Tower; i++)
        {
            if (i == cameraIndex)
            {
                curCameraObj[(int)i].rotation = transform.rotation;
            }
        }
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
