using UnityEngine;
using static MoveCamera;

public class RotateCamera : MonoBehaviour
{
    public Transform[] curCameraObj;
    #region CameraRotate
    public float rotSpeed; //감도

    private float limitMaxY = 50; //카메라 Y축 회전 범위(최대)
    private float limitMinY = -17; //카메라 Y축 회전 범위(최소)

    private float liminMaxX = default;
    private float liminMinX = default;

    private float eulerAngleX; // 마우스 좌 / 우 이동으로 카메라 y축 회전
    private float eulerAngleY; // 마우스 위 / 아래 이동으로 카메라 x축 회전
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
