using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class MoveCamera : MonoBehaviour
{
    public static MoveCamera Instance;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject[] cameras;

    #region CameraPos
    private Vector3 OriginalPos = new Vector3(0, 3f, -9f);

    private Vector3 CenterPos = new Vector3(0, 3.5f, -3.3f);

    private Vector3 RightUpPos = new Vector3(3.9f, 5.18f, -2.3f);
    private Vector3 RightUpRot = new Vector3(-6.4f, 35, 0);

    private Vector3 LeftUpPos = new Vector3(-3.9f, 5.18f, -2.3f);
    private Vector3 LeftUpRot = new Vector3(-6.4f, -35, 0);

    private Vector3 RightDownPos = new Vector3(4f, 2.3f, -2.3f);
    private Vector3 RightDownRot = new Vector3(5.8f, 34f, 0f);

    private Vector3 LeftDownPos = new Vector3(-4f, 2.3f, 2.3f);
    private Vector3 LeftDownRot = new Vector3(5.8f, -34f, 0f);
    #endregion 

    public float Time = 0;

    public enum CameraState
    {
        Original, Center, RightUp, LeftUp, RightDown, LeftDown
    }

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { CurrentState = CameraState.Original; }
    }

    [SerializeField] private CameraState currentState = CameraState.Original;
    public CameraState CurrentState { get { return currentState; } set { currentState = value; CameraMove(); } }

    private void SwitchCamera(MoveCamera.CameraState cameraIndex)
    {
        for (MoveCamera.CameraState i = MoveCamera.CameraState.Original; i <= MoveCamera.CameraState.RightDown; i++)
        {
            if (i == cameraIndex)
            {
                cameras[(int)i].SetActive(true);
            }
            else
            {
                cameras[(int)i].SetActive(false);
            }
        }
    }

    private void CameraMove()
    {
        switch (currentState)
        {
            case CameraState.Original:
                mainCamera.transform.DOMove(OriginalPos, Time).SetEase(Ease.OutExpo);
                mainCamera.transform.DORotate(new Vector3(0, 0, 0), Time).SetEase(Ease.OutExpo);
                SwitchCamera(CameraState.Original);
                break;
            case CameraState.Center:
                {
                    mainCamera.transform.DOMove(CenterPos, Time).SetEase(Ease.OutExpo);
                    SwitchCamera(CameraState.Center);
                };
                break;
            case CameraState.RightUp:
                {
                    mainCamera.transform.DOMove(RightUpPos, Time).SetEase(Ease.OutExpo);
                    mainCamera.transform.DORotate(RightUpRot, Time).SetEase(Ease.OutExpo);
                    SwitchCamera(CameraState.RightUp);
                };
                break;
            case CameraState.LeftUp:
                {
                    mainCamera.transform.DOMove(LeftUpPos, Time).SetEase(Ease.OutExpo);
                    mainCamera.transform.DORotate(LeftUpRot, Time).SetEase(Ease.OutExpo);
                    SwitchCamera(CameraState.LeftUp);
                };
                break;
            case CameraState.RightDown:
                {
                    mainCamera.transform.DOMove(RightDownPos, Time).SetEase(Ease.OutExpo);
                    mainCamera.transform.DORotate(RightDownRot, Time).SetEase(Ease.OutExpo);
                    SwitchCamera(CameraState.RightDown);
                }
                break;
            case CameraState.LeftDown:
                {
                    mainCamera.transform.DOMove(LeftDownPos, Time).SetEase(Ease.OutExpo);
                    mainCamera.transform.DORotate(LeftDownRot, Time).SetEase(Ease.OutExpo);
                    SwitchCamera(currentState);
                }
                break;
        }
    }
}
