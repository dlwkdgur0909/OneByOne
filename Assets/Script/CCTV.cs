using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class CCTV : MonoBehaviour
{
    [SerializeField] private Camera curCamera;
    [SerializeField] private MoveCamera.CameraState curState;


    private Vector3 previousRotation;

    public void OnMouseDown()
    {
        previousRotation = curCamera.transform.eulerAngles;
        MoveCamera.Instance.CurrentState = curState;
    }

    public void AfterStateChange()
    {
        // 상태 변경 후 이전 회전값으로 복구
        curCamera.transform.eulerAngles = previousRotation;
    }
}
