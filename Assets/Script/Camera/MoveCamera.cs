using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MoveCamera : MonoBehaviour
{
    public static MoveCamera Instance;
    [SerializeField] private Camera mainCamera;
    public GameObject[] cameras;

    #region CameraPos
    public GameObject playerPos;
    private Vector3 OriginalRot = new Vector3(0f, 0f, 0f);

    private Vector3 CenterPos = new Vector3(0, 10.5f, -3.3f);

    private Vector3 RightPos = new Vector3(4f, 12.31f, -2.3f);
    private Vector3 RightRot = new Vector3(-4.2f, 34, 0);

    private Vector3 LeftPos = new Vector3(-4f, 12.31f, -2.3f);
    private Vector3 LeftRot = new Vector3(-4.2f, -34, 0);

    private Vector3 TowerPos = new Vector3(3.8f, 9.3f, -2.26f);
    private Vector3 TowerRot = new Vector3(5.6f, 35.6f, 0f);
    #endregion

    //���� ���°� CCTV�� �����ִ� �������� Ȯ���ϴ� ����
    public bool isOnCamera = false;

    public enum CameraState
    {
        Original, Center, Right, Left, Tower
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentState = CameraState.Original;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveCameraState(currentState);
            mainCamera.transform.rotation = Quaternion.Euler(OriginalRot);
            CurrentState = CameraState.Original;
        }
    }

    //cameraIndex�� ���� ���� ī�޶� ���õ� cameraIndex�� �̵�
    IEnumerator CameraChange(CameraState cameraIndex)
    {
        yield return new WaitForSeconds(1f);
        for (CameraState i = CameraState.Original; i <= CameraState.Tower; ++i)
        {
            if (i == cameraIndex)
            {
                if (i == CameraState.Original)
                {
                    mainCamera.transform.position = cameras[(int)i].transform.position;
                    mainCamera.transform.localRotation = cameras[(int)i].transform.localRotation;
                }
                mainCamera.transform.position = cameras[(int)i].transform.position;
                mainCamera.transform.localRotation = cameras[(int)i].transform.localRotation;
                isOnCamera = true;
            }
        }
    }

    //�����̽��ٸ� ������ �� ���õ� cameraIndex�� ȸ������ ����ī�޶󿡰� ������
    public IEnumerator SaveCameraState(CameraState cameraIndex)
    {
        yield return new WaitForSeconds(1.1f);
        for (CameraState i = CameraState.Original; i <= CameraState.Tower; ++i)
        {
            if (i == cameraIndex)
            {
                if (i == CameraState.Original) isOnCamera = false;
                else cameras[(int)i].transform.rotation = mainCamera.transform.rotation;
            }
        }
    }

    [SerializeField] public CameraState currentState = CameraState.Original;
    public CameraState CurrentState { get { return currentState; } set { currentState = value; CameraMove(); } }

    private void CameraMove()
    {
        switch (currentState)
        {
            case CameraState.Original:
                mainCamera.transform.DOMove(playerPos.transform.position, 0.0001f).SetEase(Ease.OutExpo);
                mainCamera.transform.DORotate(new Vector3(0, 0, 0), 0.0001f).SetEase(Ease.OutExpo);
                StartCoroutine(CameraChange(CameraState.Original));
                StartCoroutine(SaveCameraState(CameraState.Original));
                break;
            case CameraState.Center:
                {
                    StartCoroutine(CameraChange(CameraState.Center));
                    mainCamera.transform.DOMove(CenterPos, 1).SetEase(Ease.OutExpo);
                    StartCoroutine(SaveCameraState(CameraState.Center));
                };
                break;
            case CameraState.Right:
                {
                    mainCamera.transform.DOMove(RightPos, 1).SetEase(Ease.OutExpo);
                    mainCamera.transform.DORotate(RightRot, 1).SetEase(Ease.OutExpo);
                    StartCoroutine(CameraChange(CameraState.Right));
                    StartCoroutine(SaveCameraState(CameraState.Right));
                };
                break;
            case CameraState.Left:
                {
                    mainCamera.transform.DOMove(LeftPos, 1).SetEase(Ease.OutExpo);
                    mainCamera.transform.DORotate(LeftRot, 1).SetEase(Ease.OutExpo);
                    StartCoroutine(CameraChange(CameraState.Left));
                    StartCoroutine(SaveCameraState(CameraState.Left));
                };
                break;
            case CameraState.Tower:
                {
                    mainCamera.transform.DOMove(TowerPos, 1).SetEase(Ease.OutExpo);
                    mainCamera.transform.DORotate(TowerRot, 1).SetEase(Ease.OutExpo);
                    StartCoroutine(CameraChange(CameraState.Tower));
                    StartCoroutine(SaveCameraState(CameraState.Tower));
                }
                break;
        }
    }
}
