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
    private Vector3 OriginalPos = new Vector3(0, 8f, -9f);

    private Vector3 CenterPos = new Vector3(0, 8.5f, -3.3f);

    private Vector3 RightUpPos = new Vector3(3.9f, 10.18f, -2.3f);
    private Vector3 RightUpRot = new Vector3(-6.4f, 35, 0);

    private Vector3 LeftUpPos = new Vector3(-3.9f, 10.18f, -2.3f);
    private Vector3 LeftUpRot = new Vector3(-6.4f, -35, 0);

    private Vector3 RightDownPos = new Vector3(4f, 7.3f, -2.3f);
    private Vector3 RightDownRot = new Vector3(5.8f, 34f, 0f);

    private Vector3 LeftDownPos = new Vector3(-4f, 7.3f, 2.3f);
    private Vector3 LeftDownRot = new Vector3(5.8f, -34f, 0f);


    public float Time = 0.5f;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { CurrentState = CameraState.Original; }
    }

    public enum CameraState
    {
        Original, Center, RightUp, LeftUp, RightDown, LeftDown
    }

    [SerializeField] private CameraState currentState = CameraState.Original;
    public CameraState CurrentState { get { return currentState; } set { currentState = value; CameraMove(); } }

    private void CameraMove()
    {
        switch (currentState)
        {
            case CameraState.Original:
                mainCamera.transform.DOMove(OriginalPos, Time).SetEase(Ease.OutQuad);
                mainCamera.transform.DORotate(new Vector3(0, 0, 0), Time).SetEase(Ease.OutQuad);
                break;
            case CameraState.Center:
                mainCamera.transform.DORotate(new Vector3(0, 0, 0), Time).SetEase(Ease.OutQuad);
                mainCamera.transform.DOMove(OriginalPos, Time).SetEase(Ease.OutQuad).OnComplete(() =>
                { mainCamera.transform.DOMove(CenterPos, Time).SetEase(Ease.OutQuad); });

                break;
            case CameraState.RightUp:
                mainCamera.transform.DORotate(new Vector3(0, 0, 0), Time).SetEase(Ease.OutQuad);
                mainCamera.transform.DOMove(OriginalPos, Time).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    mainCamera.transform.DOMove(RightUpPos, Time).SetEase(Ease.OutQuad);
                    mainCamera.transform.DORotate(RightUpRot, Time).SetEase(Ease.OutQuad);
                });
                break;
            case CameraState.LeftUp:
                mainCamera.transform.DORotate(new Vector3(0, 0, 0), Time).SetEase(Ease.OutQuad);
                mainCamera.transform.DOMove(OriginalPos, Time).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    mainCamera.transform.DOMove(LeftUpPos, Time).SetEase(Ease.OutQuad);
                    mainCamera.transform.DORotate(LeftUpRot, Time).SetEase(Ease.OutQuad);
                });
                break;
            case CameraState.RightDown:
                mainCamera.transform.DORotate(new Vector3(0, 0, 0), Time).SetEase(Ease.OutQuad);
                mainCamera.transform.DOMove(RightDownPos, Time).SetEase(Ease.OutQuad);
                mainCamera.transform.DORotate(RightDownRot, Time).SetEase(Ease.OutQuad);
                break;
            case CameraState.LeftDown:
                mainCamera.transform.DORotate(new Vector3(0, 0, 0), Time).SetEase(Ease.OutQuad);
                mainCamera.transform.DOMove(LeftDownPos, Time).SetEase(Ease.OutQuad);
                mainCamera.transform.DORotate(LeftDownRot, Time).SetEase(Ease.OutQuad);
                break;
        }
    }
}
