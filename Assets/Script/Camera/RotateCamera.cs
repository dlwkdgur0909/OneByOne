using UnityEngine;
using static MoveCamera;

public class RotateCamera : MonoBehaviour
{
    public Transform[] curCameraObj;
    #region CameraRotate
    public float rotSpeed; //감도

    private float limitMaxY = 50; //카메라 Y축 회전 범위(최대)
    private float limitMinY = -17; //카메라 Y축 회전 범위(최소)

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
