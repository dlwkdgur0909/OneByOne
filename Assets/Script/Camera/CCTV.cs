using UnityEngine;

public class CCTV : MonoBehaviour
{
    [SerializeField] private Camera curCamera;
    [SerializeField] private MoveCamera.CameraState curState;
    private Vector3 previousRotation;

    public void OnMouseDown()
    {
        //총을 들고있으면 카메라에 입장하지 못함
        if(mainCamera.instance.isHaveGun == false)
        {
            previousRotation = curCamera.transform.eulerAngles;
            MoveCamera.Instance.CurrentState = curState;
        }
    }

    public void AfterStateChange()
    {
        // 상태 변경 후 이전 회전값으로 복구
        curCamera.transform.eulerAngles = previousRotation;
    }
}
