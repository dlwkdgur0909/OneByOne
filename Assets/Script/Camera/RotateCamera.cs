using UnityEngine;
using static MoveCamera;

public class RotateCamera : MonoBehaviour
{
    public Transform[] curCameraObj;
    #region CameraRotate
    public float rotSpeed; //����

    private float limitMaxY = 50; //ī�޶� Y�� ȸ�� ����(�ִ�)
    private float limitMinY = -17; //ī�޶� Y�� ȸ�� ����(�ּ�)

    private float liminMaxX = default;
    private float liminMinX = default;

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
                case CameraState.Original:
                    {
                        liminMaxX = 360;
                        liminMinX = -360;
                        ChoiceCamera(CameraState.Original); break;
                    }
                case CameraState.Center:
                    {
                        liminMaxX = 270;
                        liminMinX = 90;
                        ChoiceCamera(CameraState.Center); break;
                    }
                case CameraState.Right:
                    {
                        liminMaxX = 180;
                        liminMinX = 0;
                        ChoiceCamera(CameraState.Right); break;
                    }
                case CameraState.Left:
                    {
                        liminMaxX = 0;
                        liminMinX = -180;
                        ChoiceCamera(CameraState.Left); break;
                    }
                case CameraState.Tower:
                    {
                        liminMaxX = 90;
                        liminMinX = -90;
                        ChoiceCamera(CameraState.Tower); break;
                    }
            }

            transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
            if (curCameraObj != null)
            {
                ChoiceCamera(Instance.currentState);
            }
        }
    }

    public void ChoiceCamera(CameraState cameraIndex)
    {
        eulerAngleY = ClampAngle(eulerAngleY, liminMinX, liminMaxX);

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
