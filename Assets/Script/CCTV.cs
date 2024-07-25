using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static MoveCamera;

public class CCTV : MonoBehaviour
{
    [SerializeField] private Camera curCamera;
    [SerializeField] private MoveCamera.CameraState curState;
    public float rotSpeed = 200f;
    float mx;
    float my;
    //�ȳ��ϼ��� �����Դϴ�. ���� �ٺ��֤Ŀ�.
    private Vector3 previousRotation;

    public void Update()
    {
        if (MoveCamera.Instance.CurrentState == curState)
        {

            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");

            mx += h * rotSpeed * Time.deltaTime;
            my += v * rotSpeed * Time.deltaTime;

            if (my >= 90)
            {
                my = 90;
            }
            else if (my <= -90)
            {
                my = -90;
            }
            curCamera.transform.eulerAngles = new Vector3(-my, mx, 0);
        }
    }

    public void OnMouseDown()
    {
        previousRotation = curCamera.transform.eulerAngles;

        MoveCamera.Instance.CurrentState = curState;
    }

    public void AfterStateChange()
    {
        // ���� ���� �� ���� ȸ�������� ����
        curCamera.transform.eulerAngles = previousRotation;
    }
}
