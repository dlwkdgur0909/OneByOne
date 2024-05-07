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
    public GameObject[] cameras;

    #region CameraPos
    private Vector3 OriginalPos = new Vector3(0, 3f, -9f);

    private Vector3 CenterPos = new Vector3(0, 3.5f, -3.3f);

    private Vector3 RightUpPos = new Vector3(3.9f, 5.18f, -2.3f);
    private Vector3 RightUpRot = new Vector3(-6.4f, 35, 0);

    private Vector3 LeftUpPos = new Vector3(-3.9f, 5.18f, -2.3f);
    private Vector3 LeftUpRot = new Vector3(-6.4f, -35, 0);

    private Vector3 TowerPos = new Vector3(4f, 2.3f, -2.3f);
    private Vector3 TowerRot = new Vector3(5.8f, 34f, 0f);
    #endregion 

    public float Time = 0;

    public enum CameraState
    {
        Original, Center, RightUp, LeftUp, Tower
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

    IEnumerator CameraChange(CameraState cameraIndex)
    {
        yield return new WaitForSeconds(1f);
        for(CameraState i = CameraState.Original; i <= CameraState.Tower; ++i) 
        {
            if(i == cameraIndex)
            {
                mainCamera.transform.position = cameras[(int)i].transform.position;
                mainCamera.transform.localRotation = cameras[(int)i].transform.localRotation;
            }
        }
    }

    private void RotateCamera()
    {
        //카메라 움직이는 기능 넣기
    }

    private void CameraMove()
    {
        switch (currentState)
        {
            case CameraState.Original:
                mainCamera.transform.DOMove(OriginalPos, 0.0001f).SetEase(Ease.OutExpo);
                mainCamera.transform.DORotate(new Vector3(0, 0, 0), 0.0001f).SetEase(Ease.OutExpo);
                StartCoroutine(CameraChange(CameraState.Original));
                break;
            case CameraState.Center:
                {
                    mainCamera.transform.DOMove(CenterPos, Time).SetEase(Ease.OutExpo);
                    StartCoroutine(CameraChange(CameraState.Center));
                };
                break;
            case CameraState.RightUp:
                {
                    mainCamera.transform.DOMove(RightUpPos, Time).SetEase(Ease.OutExpo);
                    mainCamera.transform.DORotate(RightUpRot, Time).SetEase(Ease.OutExpo);
                    StartCoroutine(CameraChange(CameraState.RightUp));
                };
                break;
            case CameraState.LeftUp:
                {
                    mainCamera.transform.DOMove(LeftUpPos, Time).SetEase(Ease.OutExpo);
                    mainCamera.transform.DORotate(LeftUpRot, Time).SetEase(Ease.OutExpo);
                    StartCoroutine(CameraChange(CameraState.LeftUp));
                };
                break;
            case CameraState.Tower:
                {
                    mainCamera.transform.DOMove(TowerPos, Time).SetEase(Ease.OutExpo);
                    mainCamera.transform.DORotate(TowerRot, Time).SetEase(Ease.OutExpo);
                    StartCoroutine(CameraChange(CameraState.Tower));
                }
                break;
        }
    }
}
