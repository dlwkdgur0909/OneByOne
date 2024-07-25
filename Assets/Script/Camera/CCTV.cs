using UnityEngine;

public class CCTV : MonoBehaviour
{
    [SerializeField] private Camera curCamera;
    [SerializeField] private MoveCamera.CameraState curState;
    private Vector3 previousRotation;

    public void OnMouseDown()
    {
        previousRotation = curCamera.transform.eulerAngles;
        MoveCamera.Instance.CurrentState = curState;
        AudioManager.instance.CCTV.Play();
    }

    public void AfterStateChange()
    {
        // ���� ���� �� ���� ȸ�������� ����
        curCamera.transform.eulerAngles = previousRotation;
    }
}
