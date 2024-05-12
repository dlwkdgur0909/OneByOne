using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;

public class MoveCamera : MonoBehaviour
{
    public static MoveCamera Instance;
    [SerializeField] private Camera mainCamera;
    public GameObject[] cameras;

    #region CameraPos
    private Vector3 OriginalPos = new Vector3(0, 3f, -9f);
    private Vector3 OriginalRot = new Vector3(0f, 0f, 0f);

    private Vector3 CenterPos = new Vector3(0, 3.5f, -3.3f);

    private Vector3 RightPos = new Vector3(3.9f, 5.18f, -2.3f);
    private Vector3 RightRot = new Vector3(-6.4f, 35, 0);

    private Vector3 LeftPos = new Vector3(-3.9f, 5.18f, -2.3f);
    private Vector3 LeftRot = new Vector3(-6.4f, -35, 0);

    private Vector3 TowerPos = new Vector3(4f, 2.3f, -2.3f);
    private Vector3 TowerRot = new Vector3(5.8f, 34f, 0f);
    #endregion

    public float Time = 0;

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentState = CameraState.Original;
    }

    private void Update()
    {
        SaveCameraState(currentState);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainCamera.transform.rotation = Quaternion.Euler(OriginalRot);
            CurrentState = CameraState.Original;
        }
    }

    [SerializeField] private CameraState currentState = CameraState.Original;
    public CameraState CurrentState { get { return currentState; } set { currentState = value; CameraMove(); } }

    IEnumerator CameraChange(CameraState cameraIndex)
    {
        yield return new WaitForSeconds(1f);
        for (CameraState i = CameraState.Original; i <= CameraState.Tower; ++i)
        {
            if (i == cameraIndex)
            {
                mainCamera.transform.position = cameras[(int)i].transform.position;
                mainCamera.transform.localRotation = cameras[(int)i].transform.localRotation;
            }
        }
    }

    public void SaveCameraState(CameraState cameraIndex)
    {
        for (CameraState i = CameraState.Original; i <= CameraState.Tower; ++i)
        {
            if (i == cameraIndex)
            {
                cameras[(int)i].transform.rotation = mainCamera.transform.rotation;
            }
        }
    }

    private void CameraMove()
    {
        switch (currentState)
        {
            case CameraState.Original:
                mainCamera.transform.DOMove(OriginalPos, 0.0001f).SetEase(Ease.OutExpo);
                mainCamera.transform.DORotate(new Vector3(0, 0, 0), 0.0001f).SetEase(Ease.OutExpo);
                StartCoroutine(CameraChange(CameraState.Original));
                SaveCameraState(CameraState.Original);
                break;
            case CameraState.Center:
                {
                    mainCamera.transform.DOMove(CenterPos, Time).SetEase(Ease.OutExpo);
                    StartCoroutine(CameraChange(CameraState.Center));
                    SaveCameraState(CameraState.Center);
                };
                break;
            case CameraState.Right:
                {
                    mainCamera.transform.DOMove(RightPos, Time).SetEase(Ease.OutExpo);
                    mainCamera.transform.DORotate(RightRot, Time).SetEase(Ease.OutExpo);
                    StartCoroutine(CameraChange(CameraState.Right));
                    SaveCameraState(CameraState.Right);
                    break;
                };
            case CameraState.Left:
                {
                    mainCamera.transform.DOMove(LeftPos, Time).SetEase(Ease.OutExpo);
                    mainCamera.transform.DORotate(LeftRot, Time).SetEase(Ease.OutExpo);
                    StartCoroutine(CameraChange(CameraState.Left));
                    SaveCameraState(CameraState.Left);
                };
                break;
            case CameraState.Tower:
                {
                    mainCamera.transform.DOMove(TowerPos, Time).SetEase(Ease.OutExpo);
                    mainCamera.transform.DORotate(TowerRot, Time).SetEase(Ease.OutExpo);
                    StartCoroutine(CameraChange(CameraState.Tower));
                    SaveCameraState(CameraState.Tower);
                }
                break;
        }
    }
}
